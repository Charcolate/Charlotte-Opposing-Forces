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
    public float blueDisplayTime = 2f; // Time to display solid blue

    private SpriteRenderer sr;
    private float fadeTimer;
    private bool isFading = true;
    private float blueTimer;

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

        // Set initial red color with transparency
        sr.color = new Color(1f, 0f, 0f, initialAlpha);
        fadeTimer = fadeDuration;
        isFading = true;
        blueTimer = blueDisplayTime;
    }

    protected override void OnUpdate()
    {
        if (sr == null)
        {
            EndAction(false);
            return;
        }

        if (isFading)
        {
            fadeTimer -= Time.deltaTime;

            if (fadeTimer > 0f)
            {
                // Continue fading
                float alpha = Mathf.Clamp01(fadeTimer / fadeDuration) * initialAlpha;
                sr.color = new Color(1f, 0f, 0f, alpha);
            }
            else
            {
                // Fade complete, turn solid blue
                sr.color = Color.blue;
                isFading = false;
            }
        }
        else
        {
            blueTimer -= Time.deltaTime;
            if (blueTimer <= 0f)
            {
                // Blue display time complete
                EndAction(true);
            }
        }
    }

    protected override void OnStop()
    {
        // Reset the color when the action stops
        sr.color = new Color(1f, 0f, 0f);
    }
}
