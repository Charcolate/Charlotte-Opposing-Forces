using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

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
