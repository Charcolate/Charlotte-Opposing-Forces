using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBothy : ActionTask
{
    public BBParameter<GameObject> Red;
    public BBParameter<GameObject> Purple;
    public BBParameter<float> delayBetweenDestructions = 0f;

    private bool firstDestroyed;

    protected override void OnExecute()
    {
        firstDestroyed = false;
        if (Red.value != null)
        {
            GameObject.Destroy(Red.value);
        }
        firstDestroyed = true;

        if (delayBetweenDestructions.value <= 0)
        {
            DestroyPurple();
            EndAction(true);
        }
    }

    protected override void OnUpdate()
    {
        if (firstDestroyed && elapsedTime >= delayBetweenDestructions.value)
        {
            DestroyPurple();
            EndAction(true);
        }
    }

    void DestroyPurple()
    {
        if (Purple.value != null)
        {
            GameObject.Destroy(Purple.value);
        }
    }

}
