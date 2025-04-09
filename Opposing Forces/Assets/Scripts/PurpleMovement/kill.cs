using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

public class kill : ActionTask<Transform>
{
    public GameObject bulletPrefab;
    public GameObject player;
    public float bulletSpeed = 5f;
    public float collisionDistance = 0.2f;

    private GameObject bulletInstance;
    private Vector3 direction;
    private bool bulletFired = false;

    protected override void OnExecute()
    {
        if (bulletPrefab == null || player == null)
        {
            Debug.LogWarning("BulletPrefab or Player not assigned.");
            EndAction(false);
            return;
        }

        // Instantiate the bullet and calculate direction
        bulletInstance = GameObject.Instantiate(bulletPrefab, agent.position, Quaternion.identity);
        direction = (player.transform.position - agent.position).normalized;
        bulletFired = true;
    }

    protected override void OnUpdate()
    {
        if (!bulletFired || bulletInstance == null || player == null)
        {
            EndAction(false);
            return;
        }

        // Move the bullet manually
        bulletInstance.transform.position += direction * bulletSpeed * Time.deltaTime;

        // Check for collision
        float distanceToPlayer = Vector2.Distance(bulletInstance.transform.position, player.transform.position);
        if (distanceToPlayer <= collisionDistance)
        {
            GameObject.Destroy(player);
            EndAction(true);
        }

        // Optionally end action if bullet goes too far (e.g. off screen)
        if (Vector2.Distance(bulletInstance.transform.position, agent.position) > 20f)
        {
            EndAction(false);
        }
    }

    protected override void OnStop()
    {
        if (bulletInstance != null)
        {
            GameObject.Destroy(bulletInstance);
        }
    }
}

