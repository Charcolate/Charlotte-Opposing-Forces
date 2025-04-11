using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class ChangeColour : ActionTask
{
    public BBParameter<GameObject> targetObject;
    public BBParameter<Color> newColor;
    public float fadeDuration = 0f;

    private Renderer targetRenderer;
    private Color originalColor;
    private float t;

    protected override string info
    {
        get
        {
            return $"Change {targetObject}'s Color to {newColor}" +
            (fadeDuration > 0 ? $" over {fadeDuration}s" : "");
        }
    }

    protected override void OnExecute()
    {
        if (targetObject.value == null)
        {
            EndAction(false);
            return;
        }

        targetRenderer = targetObject.value.GetComponent<Renderer>();
        if (targetRenderer == null)
        {
            Debug.LogWarning("No Renderer component found on target object", targetObject.value);
            EndAction(false);
            return;
        }

        originalColor = targetRenderer.material.color;
        t = 0f;

        if (fadeDuration <= 0)
        {
            targetRenderer.material.color = newColor.value;
            EndAction(true);
        }
    }

    protected override void OnUpdate()
    {
        if (targetRenderer == null)
        {
            EndAction(false);
            return;
        }

        t += Time.deltaTime / fadeDuration;
        targetRenderer.material.color = Color.Lerp(originalColor, newColor.value, t);

        if (t >= 1f)
        {
            EndAction(true);
        }
    }
}
