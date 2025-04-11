using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class MoveTarget : ActionTask<Transform>
{
    public BBParameter<Transform> player;
    public BBParameter<float> speed = 10f;
    public BBParameter<float> CatchPlayer = 0.5f;

    private Rigidbody2D rb;
    private bool reachedPlayer;

    protected override string OnInit()
    {
        rb = agent.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found!", agent.gameObject);
            return "Error: Missing Rigidbody2D";
        }
        return null;
    }

    protected override void OnExecute()
    {
        reachedPlayer = false;
    }

    protected override void OnUpdate()
    {
        if (player.value == null)
        {
            EndAction(false);
            return;
        }

        Vector2 toPlayer = player.value.position - agent.position;
        float distance = toPlayer.magnitude;

        // Continuous tracking until within stopping distance
        if (distance > CatchPlayer.value)
        {
            // Normalize only if we need to move (optimization)
            Vector2 direction = toPlayer.normalized;
            rb.velocity = direction * speed.value;
        }
        else
        {
            if (!reachedPlayer)
            {
                reachedPlayer = true;
                rb.velocity = Vector2.zero;
                EndAction(true); // Success - proceed to next node
            }
        }
    }

    // Clean up if interrupted
    protected override void OnStop()
    {
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }
    }

    public override void OnDrawGizmosSelected()
    {
        if (agent != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(agent.position, CatchPlayer.value);
        }
    }

}
