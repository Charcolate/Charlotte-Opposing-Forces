using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSignifier : ActionTask
{
    public BBParameter<GameObject> Icon;
    public BBParameter<float> duration = 2f;

    protected override void OnExecute()
    {
        if (Icon.value != null)
        {
            Icon.value.SetActive(true);
            StartCoroutine(HideAfterSeconds());
        }
        else
        {
            Debug.LogWarning("Icon is not assigned.");
            EndAction(false);
        }
    }

    private IEnumerator HideAfterSeconds()
    {
        yield return new WaitForSeconds(duration.value);
        Icon.value.SetActive(false);
        EndAction(true);
    }

}
