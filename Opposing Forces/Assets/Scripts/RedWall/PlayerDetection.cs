using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class PlayerDetection : ConditionTask<Transform>
{
    public BBParameter<GameObject> player;

    public float detectionRadius = 5f;

    //  Check location of player
    protected override bool OnCheck()
    {
        if (player.value == null)
        {
            Debug.LogWarning("Player GameObject is not assigned.");
            return false;
        }

        float distance = Vector3.Distance(agent.position, player.value.transform.position);
        return distance <= detectionRadius;
    }

    // Draw detection radius in editor
    public override void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.position, detectionRadius);
        }
    }

}
