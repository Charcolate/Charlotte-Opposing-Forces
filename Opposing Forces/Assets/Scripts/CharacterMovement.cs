using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class CharacterMovement : MonoBehaviour

    //GREEN DOES NOT WORK SO INCASE OF BUG, IT IS NOT GOING TO BE RELATED 
{
    [SerializeField] private float moveSpeed = 5f; // Base movement speed
                                                   //[SerializeField] private float coinAttractionRange = 3f; // How close a coin needs to be to attract player
                                                   //[SerializeField] private float attractionDuration = 3f; // How long attraction lasts (3 seconds)
                                                   //[SerializeField] private float waypointDistanceThreshold = 0.1f; // How close player needs to be to a waypoint to advance

    //private bool isAttracted = false; // Flag for attraction state
    // private float attractionTimer = 0f; // Countdown timer for attraction duration
    // private GameObject targetCoin = null; // Reference to the attracting coin

    //private Seeker seeker; // A* Pathfinding component
    //private Path path; // Current calculated path
    //private int currentWaypoint = 0; // Index of current waypoint in path


    //private void Start()
    //{
    // Get or add the Seeker component required for pathfinding
    //    seeker = GetComponent<Seeker>();
    //    if (seeker == null)
    //    {
    //        seeker = gameObject.AddComponent<Seeker>();
    //    }
    //}

    //private void Update()
    //{
    // State machine: either handle attraction movement or normal movement
    //    if (isAttracted)
    //    {
    //        HandleAttractionMovement();
    //        attractionTimer -= Time.deltaTime;
    // Update and check attraction timer
    //        if (attractionTimer <= 0f)
    //        {
    //            EndAttraction();
    //        }
    //    }
    //    else
    //    {
    //        HandleNormalMovement();
    //        CheckForNearbyCoins();
    //    }
    //}

    // Player movement with WASD/arrow keys
    //private void HandleNormalMovement()
    //{
    //    float horizontalInput = Input.GetAxisRaw("Horizontal");
    //    float verticalInput = Input.GetAxisRaw("Vertical");

    //    Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;
    //    transform.Translate(movement * moveSpeed * Time.deltaTime);
    //}

    // Checks for coins within attraction range
    //private void CheckForNearbyCoins()
    //{
    // Skip if already attracted
    //    if (isAttracted) return;
    // Detect all colliders in range
    //    Collider2D[] nearbyCoins = Physics2D.OverlapCircleAll(transform.position, coinAttractionRange);
    // Check each detected collider for Coin tag
    //    foreach (Collider2D coin in nearbyCoins)
    //    {
    //        if (coin.CompareTag("Coin"))
    //        {
    //            StartAttraction(coin.gameObject);
    //            break; // Only attract to one coin at a time
    //        }
    //    }
    //}

    // Starts the coin attraction sequence
    //private void StartAttraction(GameObject coin)
    //{
    //    isAttracted = true;
    //    attractionTimer = attractionDuration;
    //    targetCoin = coin;

    //    // Start pathfinding to the coin
    //    seeker.StartPath(transform.position, targetCoin.transform.position, OnPathComplete);
    //}

    // Movement during attraction state
    //private void HandleAttractionMovement()
    //{
    // Don't move even if no path or path is complete (after 3s mark)
    //    if (path == null || currentWaypoint >= path.vectorPath.Count)
    //    {
    //        return;
    //    }

    // Calculate direction to current waypoint
    //    // Calculate direction to current waypoint
    //    Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - (Vector2)transform.position).normalized;
    //    transform.Translate(direction * moveSpeed * Time.deltaTime);

    //    // Check if reached current waypoint
    //    if (Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]) < waypointDistanceThreshold)
    //    {
    //        currentWaypoint++;
    //    }
    //}

    // Cleans up after attraction ends
    //private void EndAttraction()
    //{
    //    isAttracted = false;
    //    targetCoin = null;
    //    path = null;
    //    currentWaypoint = 0;
    //}

    // Callback when path calculation completes
    //private void OnPathComplete(Path p)
    //{
    //    if (!p.error)
    //    {
    //        path = p;
    //        currentWaypoint = 0;
    //    }
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, coinAttractionRange);

    //    if (isAttracted && path != null)
    //    {
    //        Gizmos.color = Color.red;
    //        for (int i = 0; i < path.vectorPath.Count - 1; i++)
    //        {
    //            Gizmos.DrawLine(path.vectorPath[i], path.vectorPath[i+1]);
    //        }
    //    }
    //}


    private void Update()
    {
        // Get WASD or Arrow Key input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Create movement vector
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Normalize to prevent faster diagonal movement
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Move the player
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
}
