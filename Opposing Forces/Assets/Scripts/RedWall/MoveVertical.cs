using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class MoveVertical : ActionTask
{
    public BBParameter<GameObject> wallObject;
    public BBParameter<Vector3> originalPosition; // Blackboard variable to store position
    public float moveDistance = 2f;
    public float moveSpeed = 2f;
    public bool moveUp = true;

    private Vector3 targetPos;

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

        targetPos = originalPosition.value + Vector3.up * (moveUp ? moveDistance : -moveDistance);
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

