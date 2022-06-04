using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Canvas inventoryScreen;

    Vector3 velocity = new Vector2();
    Rigidbody2D rb;
    Camera cam;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        inventoryScreen.enabled = false;
    }

    void Update()
    {
        LookAtMousePosition();
        HandleMovement();
        ToggleInventoryScreen();
    }

    private void ToggleInventoryScreen()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) inventoryScreen.enabled = !inventoryScreen.enabled;
    }

    private void HandleMovement()
    {
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

        transform.position += moveSpeed * Time.deltaTime * velocity;
    }

    private void LookAtMousePosition()
    {
        Vector3 diff = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }
    
}
