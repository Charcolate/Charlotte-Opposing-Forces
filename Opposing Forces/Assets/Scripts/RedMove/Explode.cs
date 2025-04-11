using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : ActionTask<Transform>
{
    public BBParameter<float> radius = 3f;
    public BBParameter<GameObject> Target; // Drag your target here

    protected override void OnExecute()
    {
        if (Target.value == null)
        {
            EndAction(false);
            return;
        }

        float distance = Vector2.Distance(agent.position, Target.value.transform.position);

        if (distance <= radius.value)
        {
            GameObject.Destroy(Target.value);
            EndAction(true);
        }
        else
        {
            EndAction(false);
        }
    }

    public override void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(agent.position, radius.value);

            if (Target.value != null)
            {
                Gizmos.DrawLine(agent.position, Target.value.transform.position);
            }
        }
    }
}
