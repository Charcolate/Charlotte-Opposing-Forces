using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class ActivateAllWalls : ActionTask
{
    // Red Vertical Wall Parameters
    public BBParameter<GameObject> redWall;
    public BBParameter<Vector3> redWallOriginalPos;
    public float verticalMoveDistance = 2f;
    public float verticalMoveSpeed = 2f;
    public bool moveUp = true;
    private Vector3 redTargetPos;

    // Purple Horizontal Wall Parameters
    public BBParameter<GameObject> purpleWall;
    public float disappearDuration = 2f;
    private float disappearTimer;

    protected override void OnExecute()
    {
        // Set up red wall movement
        if (redWall.value != null)
        {
            redWallOriginalPos.value = redWall.value.transform.position;
            redTargetPos = redWallOriginalPos.value +
                          Vector3.up * (moveUp ? verticalMoveDistance : -verticalMoveDistance);

        }

        // Set up purple wall disappearance
        if (purpleWall.value != null)
        {
            // Make it disappear immediately
            purpleWall.value.SetActive(false);
            disappearTimer = 0f;
        }
    }

    protected override void OnUpdate()
    {
        // Handle red wall movement
        if (redWall.value != null)
        {
            redWall.value.transform.position = Vector3.MoveTowards(
                redWall.value.transform.position,
                redTargetPos,
                verticalMoveSpeed * Time.deltaTime
            );
        }

        // Handle purple wall disappearance timer
        if (purpleWall.value != null)
        {
            disappearTimer += Time.deltaTime;
        }

        // End action when both complete:
        // - Red wall reaches target
        // - Purple wall has disappeared for duration 
        bool redComplete = redWall.value == null ||
                         Vector3.Distance(redWall.value.transform.position, redTargetPos) < 0.01f;

        bool purpleComplete = purpleWall.value == null ||
                            disappearTimer >= disappearDuration;

        if (redComplete && purpleComplete)
        {
            EndAction(true);
        }
    }
}
