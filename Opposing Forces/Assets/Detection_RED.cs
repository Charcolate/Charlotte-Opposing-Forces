using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class Detection_RED : ActionTask
{
    public BBParameter<GameObject> redFlashObject;

    public float fadeDuration = 0.2f;
    public float initialAlpha = 0.5f;

    private SpriteRenderer sr;
    private float timer;

    protected override void OnExecute()
    {
        if (redFlashObject.value == null)
        {
            Debug.LogWarning("RedFlashObject not assigned!");
            EndAction(false);
            return;
        }

        sr = redFlashObject.value.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("RedFlashObject has no SpriteRenderer!");
            EndAction(false);
            return;
        }

        // Set initial flash
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, initialAlpha);
        timer = fadeDuration;
    }

    protected override void OnUpdate()
    {
        if (sr == null)
        {
            EndAction(false);
            return;
        }

        timer -= Time.deltaTime;
        float alpha = Mathf.Clamp01(timer / fadeDuration) * initialAlpha;
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, alpha);

        if (timer <= 0f)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0f); // fully invisible
            EndAction(true);
        }
    }
}
