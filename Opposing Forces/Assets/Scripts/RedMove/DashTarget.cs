using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class DashTarget : ActionTask
{
    public BBParameter<Transform> target;
    public BBParameter<float> dashSpeed = 20f;
    public BBParameter<float> dashDuration = 0.5f;

    private float dashTimeElapsed;
    private Vector3 dashDirection;

    protected override void OnExecute()
    {
        if (target.value == null)
        {
            EndAction(false);
            return;
        }

        dashDirection = (target.value.position - agent.transform.position).normalized;
        dashTimeElapsed = 0f;
    }

    protected override void OnUpdate()
    {
        dashTimeElapsed += Time.deltaTime;

        if (dashTimeElapsed >= dashDuration.value)
        {
            EndAction(true);
            return;
        }

        agent.transform.position += dashDirection * dashSpeed.value * Time.deltaTime;
    }
}
