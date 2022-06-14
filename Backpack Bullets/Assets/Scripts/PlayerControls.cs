using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;



[SelectionBase]
public class PlayerControls : MonoBehaviour
{
    Vector3 velocity = new Vector2();
    Camera cam;

    public static event Action<int> OnPotionKeyPress;
    public static event Action OnInventoryToggle;

    public static event Action OnPrimaryAttack;
    public static event Action OnSecondaryAttack;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!UIManager.Instance.IsInventoryOpen)
        {
            HandleMovement();
            LookAtMousePosition();
            HandleQuickbarInput();
            ProcessAttacks();
        }

        ToggleInventoryScreen();
        ReloadSandbox();    // TODO: Delete this helper function
    }

    private void HandleMovement()
    {
        float moveSpeed = StatSystem.Instance.moveSpeed;
        velocity.x = Input.GetAxis("Horizontal");
        velocity.y = Input.GetAxis("Vertical");

        transform.position += (moveSpeed / 2) * Time.deltaTime * velocity;
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

    void ProcessAttacks()
    {
        if (Input.GetMouseButton(0))
        {
            OnPrimaryAttack?.Invoke();

        }
        else if (Input.GetMouseButtonDown(1))
        {
            OnSecondaryAttack?.Invoke();
        }
        else
            GetComponent<PlayerCombat>().StopAllAttacks();
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
