using NodeCanvas.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDetection : ConditionTask
{
    public BBParameter<GameObject> player;
    public BBParameter<GameObject> detectionOrigin;
    public float detectionRadius = 5f;

    // Store previous position to track movement
    private Vector3 previousOriginPosition;
    private bool isFirstCheck = true;

    protected override bool OnCheck()
    {
        if (player.value == null)
        {
            Debug.LogWarning("Player GameObject is not assigned.");
            return false;
        }

        if (detectionOrigin.value == null)
        {
            Debug.LogWarning("Detection Origin GameObject is not assigned.");
            return false;
        }

        // Update the detection origin's movement
        if (isFirstCheck)
        {
            previousOriginPosition = detectionOrigin.value.transform.position;
            isFirstCheck = false;
        }
        else
        {
            Vector3 currentPosition = detectionOrigin.value.transform.position;
            if (currentPosition != previousOriginPosition)
            {
                // Origin has moved - we could add additional logic here if needed
                previousOriginPosition = currentPosition;
            }
        }

        // Check distance between player and current origin position
        float distance = Vector3.Distance(detectionOrigin.value.transform.position, player.value.transform.position);
        return distance <= detectionRadius;
    }

    public override void OnDrawGizmosSelected()
    {
        if (detectionOrigin.value != null && Application.isPlaying)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(detectionOrigin.value.transform.position, detectionRadius);

            // Draw a line to player if within radius
            if (player.value != null && Vector3.Distance(detectionOrigin.value.transform.position, player.value.transform.position) <= detectionRadius)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(detectionOrigin.value.transform.position, player.value.transform.position);
            }
        }
    }
}
