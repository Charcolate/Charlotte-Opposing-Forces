using UnityEngine;
using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class HoriResetWall : ActionTask
{
    public BBParameter<GameObject> wallObject;
    public BBParameter<Vector3> originalPosition;
    public float disappearDuration = 2f;

    private float timer;
    private bool isDisappeared;

    protected override void OnExecute()
    {
        if (wallObject.value == null)
        {
            Debug.LogWarning("Wall object is not assigned.");
            EndAction(false);
            return;
        }

        // Store the original position if not already stored
        originalPosition.value = wallObject.value.transform.position;

        // Make the wall disappear
        wallObject.value.SetActive(false);
        timer = 0f;
        isDisappeared = true;
    }

    protected override void OnUpdate()
    {
        if (!isDisappeared) return;

        timer += Time.deltaTime;

        if (timer >= disappearDuration)
        {
            // Make the wall reappear
            wallObject.value.SetActive(true);
            wallObject.value.transform.position = originalPosition.value;
            EndAction(true);
        }
    }
}
