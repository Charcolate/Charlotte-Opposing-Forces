using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBetweenPoints : ActionTask<Transform>
{ 
    public BBParameter<Vector2> pointA;
    public BBParameter<Vector2> pointB;
    public BBParameter<float> speed = 2f;
    public BBParameter<float> waitTimeAtPoints = 0.5f;
    public BBParameter<bool> smoothMovement = true;
    
    // Player detection parameters
    public BBParameter<Transform> player;
    public BBParameter<float> detectionDistance = 5f;
    public BBParameter<bool> drawGizmos = true;
    public BBParameter<float> detectionDelay = 1f; // Time to wait after detection

    private Vector2 currentTarget;
    private float waitTimer;
    private bool movingToB = true;
    private bool playerDetected;
    private float detectionTimer;

    protected override string info
    {
        get { return string.Format("Move Between {0} and {1}", pointA, pointB); }
    }

    protected override void OnExecute()
    {
        currentTarget = movingToB ? pointB.value : pointA.value;
        waitTimer = 0f;
        playerDetected = false;
        detectionTimer = 0f;
    }

    protected override void OnUpdate()
    {
        // First check for player detection
        if (player.value != null)
        {
            float distanceToPlayer = Vector2.Distance(agent.position, player.value.position);
            if (distanceToPlayer <= detectionDistance.value)
            {
                if (!playerDetected)
                {
                    // Just detected player - start timer
                    playerDetected = true;
                    detectionTimer = 0f;
                }
                else
                {
                    // Continue counting detection time
                    detectionTimer += Time.deltaTime;

                    // If detection time exceeded, end action
                    if (detectionTimer >= detectionDelay.value)
                    {
                        EndAction(true); // Successfully detected player
                        return;
                    }
                }
                return; // Stop patrolling while detecting player
            }
            else
            {
                playerDetected = false;
            }
        }

        // If player not detected, continue normal patrolling
        PatrolBetweenPoints();
    }

    private void PatrolBetweenPoints()
    {
        // Check if we need to wait at a point
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            return;
        }

        // Calculate direction and distance
        Vector2 currentPos = agent.position;
        Vector2 direction = (currentTarget - currentPos).normalized;
        float distance = Vector2.Distance(currentPos, currentTarget);

        // Move the agent
        if (distance > 0.1f)
        {
            if (smoothMovement.value)
            {
                // Smooth movement using MoveTowards
                agent.position = Vector2.MoveTowards(currentPos, currentTarget, speed.value * Time.deltaTime);
            }
            else
            {
                // Direct movement
                agent.position += (Vector3)(direction * speed.value * Time.deltaTime);
            }
        }
        else
        {
            // Reached target, switch direction and wait
            movingToB = !movingToB;
            currentTarget = movingToB ? pointB.value : pointA.value;
            waitTimer = waitTimeAtPoints.value;
        }
    }

    public override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        if (drawGizmos.value && agent != null)
        {
            Gizmos.color = playerDetected ? Color.red : Color.yellow;
            Gizmos.DrawWireSphere(agent.position, detectionDistance.value);
        }
    }
}
