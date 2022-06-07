using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



[SelectionBase]
public class PlayerControls : MonoBehaviour
{
    // TODO: Move moveSpeed stat into a seperate player stats script once that's created
    [SerializeField] float moveSpeed;

    Vector3 velocity = new Vector2();
    Rigidbody2D rb;
    Camera cam;

    public static event Action<int> OnPotionKeyPress;
    public static event Action OnInventoryToggle;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        if (!UIManager.Instance.IsInventoryOpen)
        {
            HandleMovement();
            LookAtMousePosition();
            HandleQuickbarInput();
        }

        ToggleInventoryScreen();
        ReloadSandbox();    // TODO: Delete this helper function
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

    void HandleQuickbarInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            OnPotionKeyPress?.Invoke(0);

        if (Input.GetKeyDown(KeyCode.E))
            OnPotionKeyPress?.Invoke(1);
    }

    void ToggleInventoryScreen()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            OnInventoryToggle?.Invoke();
        }
    }

    void ReloadSandbox()
    {
        if(Input.GetKeyDown(KeyCode.Backspace))
            SceneManager.LoadScene(0);
    }
}
