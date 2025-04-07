using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;


public class horiMovementWall : ActionTask
{
    public BBParameter<GameObject> wallObject;
    public BBParameter<Vector3> originalPosition; // Blackboard variable to store position
    public float moveDistance = 2f;
    public float moveSpeed = 2f;

    private Vector3 targetPos;
    private bool moveRight; // Will store our random direction

    protected override void OnExecute()
    {
        if (wallObject.value == null)
        {
            Debug.LogWarning("Wall object is not assigned.");
            EndAction(false);
            return;
        }

        // Store the original position in the blackboard variable
        originalPosition.value = wallObject.value.transform.position;

        // 50/50 chance to move right or left
        moveRight = Random.value > 0.5f;

        // Move right if moveRight is true, otherwise move left
        targetPos = originalPosition.value + Vector3.right * (moveRight ? moveDistance : -moveDistance);
    }

    protected override void OnUpdate()
    {
        wallObject.value.transform.position = Vector3.MoveTowards(
            wallObject.value.transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(wallObject.value.transform.position, targetPos) < 0.01f)
        {
            wallObject.value.transform.position = targetPos;
            EndAction(true);
        }
    }
}
