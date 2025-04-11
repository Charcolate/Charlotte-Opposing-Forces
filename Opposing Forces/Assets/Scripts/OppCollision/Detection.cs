using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class Detection : ConditionTask<Transform>
{
    public BBParameter<GameObject> Purple;

    public float detectionRadius;

    //  Check location of player
    protected override bool OnCheck()
    {
        if (Purple.value == null)
        {
            Debug.LogWarning("Player GameObject is not assigned.");
            return false;
        }

        float distance = Vector3.Distance(agent.position, Purple.value.transform.position);
        return distance <= detectionRadius;
    }

    // Draw detection radius in editor
    public override void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(agent.position, detectionRadius);
        }
    }
}
