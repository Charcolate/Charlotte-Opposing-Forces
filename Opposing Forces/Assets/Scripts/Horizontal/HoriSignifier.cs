using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class HoriSignifier : ActionTask
{
    public BBParameter<GameObject> PurpleFlashObject;
    public float fadeDuration = 0.2f;
    public float initialAlpha = 0.5f;
    public float GreenDisplayTime = 2f; // Time to display solid Green

    private SpriteRenderer sr;
    private float fadeTimer;
    private bool isFading = true;
    private float GreenTimer;

    protected override void OnExecute()
    {
        if (PurpleFlashObject.value == null)
        {
            Debug.LogWarning("PurpleFlashObject not assigned!");
            EndAction(false);
            return;
        }

        sr = PurpleFlashObject.value.GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("PurpleFlashObject has no SpriteRenderer!");
            EndAction(false);
            return;
        }

        // Set initial red color with transparency
        sr.color = new Color(1f, 0f, 1f, initialAlpha);
        fadeTimer = fadeDuration;
        isFading = true;
        GreenTimer = GreenDisplayTime;
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
                sr.color = new Color(1f, 0f, 1f, alpha);
            }
            else
            {
                // Fade complete, turn solid blue
                sr.color = Color.green;
                isFading = false;
            }
        }
        else
        {
            GreenTimer -= Time.deltaTime;
            if (GreenTimer <= 0f)
            {
                // Green display time complete
                EndAction(true);
            }
        }
    }

    protected override void OnStop()
    {
        // Reset the color when the action stops
        sr.color = new Color(1f, 0f, 1f);
    }
}
