using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconSignifier : ActionTask
{
    public GameObject signPrefab; 
    public Transform player;
    public float distance = 10f;
    public float timelimit = 1f;
    public Vector2 signOffset = new Vector2(0, 1f); // Offset above the agent

    private GameObject signInstance; // The actual instance we'll show/hide
    private float timer;

    protected override string OnInit()
    {
        // Create the sign instance from the prefab but keep it inactive initially
        if (signPrefab != null)
        {
            signInstance = GameObject.Instantiate(signPrefab);
            signInstance.transform.SetParent(null); // Ensure it's not a child of anything
            signInstance.SetActive(false);
        }
        return null;
    }

    protected override void OnExecute()
    {
        timer = 0;
        // Position the sign relative to the agent when action starts
        if (signInstance != null)
        {
            signInstance.transform.position = (Vector2)agent.transform.position + signOffset;
        }
    }

    protected override void OnUpdate()
    {
        if (signInstance == null || player == null)
        {
            EndAction(false);
            return;
        }

        if (Vector2.Distance(player.position, agent.transform.position) < distance)
        {
            // Keep updating position in case agent moves
            signInstance.transform.position = (Vector2)agent.transform.position + signOffset;
            signInstance.SetActive(true);

            timer += Time.deltaTime;
            if (timer >= timelimit)
            {
                signInstance.SetActive(false);
                EndAction(true);
            }
        }
        else
        {
            // Hide if player moves away
            signInstance.SetActive(false);
        }
    }

    protected override void OnStop()
    {
        // Hide sign when action stops
        if (signInstance != null)
        {
            signInstance.SetActive(false);
        }
    }

    protected override void OnPause()
    {
        // Hide sign when action pauses
        if (signInstance != null)
        {
            signInstance.SetActive(false);
        }

    }
}
