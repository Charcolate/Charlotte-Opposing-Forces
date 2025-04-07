using UnityEngine;
using System.Collections;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class HoriResetWall : ActionTask
{
    public BBParameter<GameObject> wallObject;
    public BBParameter<Vector3> originalPosition; // Same blackboard variable
    public float moveSpeed = 2f;
    public float delayBeforeReset = 5f;

    private Coroutine resetRoutine;

    protected override void OnExecute()
    {
        if (wallObject.value == null)
        {
            Debug.LogWarning("Wall object is not assigned.");
            EndAction(false);
            return;
        }

        resetRoutine = StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        yield return new WaitForSeconds(delayBeforeReset);

        while (Vector3.Distance(wallObject.value.transform.position, originalPosition.value) > 0.01f)
        {
            wallObject.value.transform.position = Vector3.MoveTowards(
                wallObject.value.transform.position,
                originalPosition.value,
                moveSpeed * Time.deltaTime
            );
            yield return null;
        }

        wallObject.value.transform.position = originalPosition.value;
        EndAction(true);
    }

    protected override void OnStop()
    {
        if (resetRoutine != null)
        {
            StopCoroutine(resetRoutine);
            resetRoutine = null;
        }
    }
}
