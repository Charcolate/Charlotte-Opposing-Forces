using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class Activation : ConditionTask
{
    protected override bool OnCheck()
    {
        // Random.value returns a float between 0.0 and 1.0
        return Random.value < 0.5f;
    }
}
