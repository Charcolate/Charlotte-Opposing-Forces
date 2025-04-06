using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class HoriPlayerDetection : ConditionTask
{
    public BBParameter<GameObject> player;
    public BBParameter<GameObject> detectionOrigin; // New BBParameter for the detection origin

    public float detectionRadius = 5f;

    // Check location of player
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

        float distance = Vector3.Distance(detectionOrigin.value.transform.position, player.value.transform.position);
        return distance <= detectionRadius;
    }

    // Draw detection radius in editor
    public override void OnDrawGizmosSelected()
    {
        if (detectionOrigin.value != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectionOrigin.value.transform.position, detectionRadius);
        }
    }

}
