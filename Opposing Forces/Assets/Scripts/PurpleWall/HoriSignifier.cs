using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class HoriSignifier : ActionTask
{
    public BBParameter<GameObject> PurpleFlashObject; // Reference to the object to be flashed (should have a SpriteRenderer)
    public float fadeDuration = 0.2f; // Duration for fading effect
    public float initialAlpha = 0.5f; // Initial alpha transparency when the object is flashed
    public float GreenDisplayTime = 2f; // Time to display solid Green

    private SpriteRenderer sr;  // Reference to the SpriteRenderer of PurpleFlashObject
    private float fadeTimer; // Timer to control the fading duration
    private bool isFading = true;  // Flag to check if we are still fading
    private float GreenTimer; // Timer to track how long the object should stay green

    protected override void OnExecute()
    {
        // Check if PurpleFlashObject is assigned
        if (PurpleFlashObject.value == null)
        {
            Debug.LogWarning("PurpleFlashObject not assigned!");
            EndAction(false); // End action with failure
            return;
        }
        // Get the SpriteRenderer component of the PurpleFlashObject
        sr = PurpleFlashObject.value.GetComponent<SpriteRenderer>();

        // If there's no SpriteRenderer, log a warning and end the action
        if (sr == null)
        {
            Debug.LogWarning("PurpleFlashObject has no SpriteRenderer!");
            EndAction(false); // End action with failure
            return;
        }

        // Set initial purple color with transparency
        sr.color = new Color(1f, 0f, 1f, initialAlpha); // Color: purple (RGB: 1,0,1) with initial alpha
        fadeTimer = fadeDuration; // Set fade timer to fade duration
        isFading = true; // Indicate that the fade process has started   
        GreenTimer = GreenDisplayTime; // Set the green display time
    }

    protected override void OnUpdate()
    {
        // If the SpriteRenderer is null, stop the action immediately
        if (sr == null)
        {
            EndAction(false); // End action with failure if SpriteRenderer is missing
            return;
        }

        // If we are still fading, decrease the fade timer and update the alpha
        if (isFading)
        {
            fadeTimer -= Time.deltaTime; // Decrease fade timer by time passed

            // Continue fading by adjusting the alpha value
            if (fadeTimer > 0f)
            {
                // Continue fading
                float alpha = Mathf.Clamp01(fadeTimer / fadeDuration) * initialAlpha;  // Gradually reduce alpha
                sr.color = new Color(1f, 0f, 1f, alpha); // Set the color to purple with the adjusted alpha
            }
            else
            // Once fading is complete, set the color to solid green
            {
                // Fade complete, turn solid blue
                sr.color = Color.green;
                isFading = false;
            }
        }
        else
        {
            // If fading is complete, start the green timer
            GreenTimer -= Time.deltaTime;// Decrease the green display time

            // If the green display time is over, end the action
            if (GreenTimer <= 0f)
            {
                // Green display time complete
                EndAction(true);
            }
        }
    }

    protected override void OnStop()
    {
        // Reset the color to purple with default alpha when the action is stopped
        sr.color = new Color(1f, 0f, 1f);
    }
}
