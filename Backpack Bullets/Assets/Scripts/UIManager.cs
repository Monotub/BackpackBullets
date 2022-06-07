using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas inventoryScreen;

    bool isInventoryOpen = false;

    public bool IsInventoryOpen => isInventoryOpen;
    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        inventoryScreen.enabled = false;
    }

    private void OnEnable()
    {
        PlayerControls.OnInventoryToggle += ToggleInventoryScreen;
    }

    private void OnDisable()
    {
        PlayerControls.OnInventoryToggle -= ToggleInventoryScreen;
    }

    void ToggleInventoryScreen()
    {
        isInventoryOpen = !isInventoryOpen;

        if (isInventoryOpen) 
            inventoryScreen.enabled = true;
        else 
            inventoryScreen.enabled = false;
    }

}
