using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;

    public Vector2 RawMovementInput { get; private set; }
    public Vector2 RawDashDirectionInput { get; private set; }
    public Vector2Int DashDirectionInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool FallThroughInput { get; private set; }
    public bool FallThroughInputStop { get; private set; }
    public bool MashInput { get; private set; }
    public bool MashInputStop { get; private set; }
    public bool PauseInput { get; private set;}
    public bool PauseInputStop { get; private set;}
    public bool InteractionInput { get; private set; }

    public bool[] AttackInputs { get; private set; }


    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    public float mashDelay = .5f;
    float mash;

    public bool isPaused;
    public GameObject pauseMenu;

    private PlayerInput playerInputComponent;

    public bool isSaveMenuOpen = false;
    public bool isTeleportMenuOpen = false;

    public GameObject memoriesMenu; // Reference to the Memories Menu GameObject

    public Elevator elevator; // Reference to the Elevator script

    public ElevatorTopButton elevatorTopButton; // Reference to the ElevatorTopButton script
    public ElevatorMiddleButton elevatorMiddleButton; // Reference to the ElevatorMiddleButton script
    public ElevatorBottomButton elevatorBottomButton; // Reference to the ElevatorBottomButton script
    public GameObject inventoryMenu; // Reference to the Inventory Menu GameObject

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        AttackInputs = new bool[count];

        cam = Camera.main;
        playerInputComponent = GetComponent<PlayerInput>();

        mash = mashDelay;

        isPaused = false;
        pauseMenu.SetActive(false);
        
    }

    private void Update()
    {   
        if (isPaused == false)
        {
            CheckJumpInputHoldTime();
            CheckDashInputHoldTime();
        }
    }

    public void OnPrimaryAttackInput(InputAction.CallbackContext context)
    {   
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (isPaused == false)
        {
            if (context.started)
            {
                AttackInputs[(int)CombatInputs.primary] = true;
            }

            if (context.canceled)
            {
                AttackInputs[(int)CombatInputs.primary] = false;
            }
        }
    }

    public void OnSecondaryAttackInput(InputAction.CallbackContext context)
    {
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        //Debug.Log("OnSecondaryAttackInput called");
        if (isPaused == false)
        {
            if (context.started)
            {
                //Debug.Log("Secondary attack started");
                AttackInputs[(int)CombatInputs.secondary] = true;
                
            }

            if (context.canceled)
            {
                //Debug.Log("Secondary attack cancelled");
                AttackInputs[(int)CombatInputs.secondary] = false;
            }
        }
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        RawMovementInput = context.ReadValue<Vector2>();

        if (isPaused == false)
        {
            if(Mathf.Abs(RawMovementInput.x) > 0.5f)
            {
                NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
            }
            else
            {
                NormInputX = 0;
            }

            if(Mathf.Abs(RawMovementInput.y) > 0.5f)
            {
                NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;
            }
            else
            {
                NormInputY = 0;
            }
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (isPaused == false)
        {
            if (context.started)
            {
                if (elevator.IsPlayerOnElevator())
                {
                    elevator.MoveUp();
                    JumpInput = false; // Prevent the player from jumping while on the elevator
                }
                else
                {
                    JumpInput = true;
                    JumpInputStop = false;
                    jumpInputStartTime = Time.time;
                }
            }

            if (context.canceled)
            {
                JumpInputStop = true;
            }
        }
    }

    public void OnFallThroughInput(InputAction.CallbackContext context)
    {   
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (isPaused == false)
        {
            if (context.started)
            {
                if (elevator.IsPlayerOnElevator())
                {
                    elevator.MoveDown();
                    // No need to prevent fall through as you mentioned crouching is fine
                }
                else
                {
                    FallThroughInput = true;
                    FallThroughInputStop = false;
                }
            }

            if (context.canceled)
            {
                FallThroughInputStop = true;
            }
        }
    }

    public void OnMashInput(InputAction.CallbackContext context)
    {   
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (isPaused == false)
        {
            if (context.started)
            {
                MashInput = true;
                MashInputStop = false;
                mash -= Time.deltaTime;
                mash = mashDelay;

                //Debug.Log("MashInput True");

                if (mash <= 0)
                {
                    //Debug.Log("MashInput Failed");
                }
            }
            if (context.canceled)
            {
                MashInput = false;
                MashInputStop = true;
                //Debug.Log("MashInput False");
            }
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {   
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (isPaused == false)
        {
            if (context.started)
            {
                DashInput = true;
                DashInputStop = false;
                dashInputStartTime = Time.time;
            }
            else if (context.canceled)
            {
                DashInputStop = true;
            }
        }
    }

    public void OnDashDirectionInput(InputAction.CallbackContext context)
    {   
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (isPaused == false)
        {
            RawDashDirectionInput = context.ReadValue<Vector2>();

            DashDirectionInput = Vector2Int.RoundToInt(RawDashDirectionInput.normalized);
        }
    }

    public void OnPauseInput(InputAction.CallbackContext context)
    {
        if (isSaveMenuOpen || isTeleportMenuOpen) return;

        if (context.started)
        {
            if (isPaused == false)
            {   
                Debug.Log("Pausing");
                isPaused = true;
                PauseInput = true;
                PauseInputStop = false;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;

            }
            else if (isPaused == true)
            {
                ResetInputActions();
                ResetInputVariables();
                isPaused = false;
                PauseInput = true;
                PauseInputStop = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1f;
            }
        }

        if (context.canceled)
        {   
            PauseInputStop = true;
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (isSaveMenuOpen || isTeleportMenuOpen) return;
        
        if (isPaused == false)
        {
            if (context.started)
            {
                InteractionInput = true;
                // Removed the IsPlayerNearButton checks. The buttons themselves will handle this.
            }
            else if (context.canceled)
            {
                InteractionInput = false;
            }
        }
    }

    public void UseInteractionInput() => InteractionInput = false;

    public void UseJumpInput() => JumpInput = false;

    public void UseDashInput() => DashInput = false;

    private void CheckJumpInputHoldTime()
    {   
        if (isPaused == false)
        {
            if (Time.time >= jumpInputStartTime + inputHoldTime)
            {
                JumpInput = false;
            }
        }
    }

    private void CheckDashInputHoldTime()
    {
        if (isPaused == false)
        {
            if(Time.time >= dashInputStartTime + inputHoldTime)
            {
                DashInput = false;
            }
        }
    }

    private void ResetInputActions()
    {
        playerInputComponent.currentActionMap["PrimaryAttack"].Reset();
        playerInputComponent.currentActionMap["SecondaryAttack"].Reset();
        playerInputComponent.currentActionMap["Movement"].Reset();
        playerInputComponent.currentActionMap["Jump"].Reset();
        playerInputComponent.currentActionMap["FallThrough"].Reset();
        playerInputComponent.currentActionMap["Mash"].Reset();
        //playerInputComponent.currentActionMap["Dash"].Reset();
        //playerInputComponent.currentActionMap["DashDirection"].Reset();
    }

    private void ResetInputVariables()
    {
        RawMovementInput = Vector2.zero;
        RawDashDirectionInput = Vector2.zero;
        DashDirectionInput = Vector2Int.zero;
        NormInputX = 0;
        NormInputY = 0;
        JumpInput = false;
        JumpInputStop = false;
        DashInput = false;
        DashInputStop = false;
        FallThroughInput = false;
        FallThroughInputStop = false;
        MashInput = false;
        MashInputStop = false;
        PauseInput = false;
        PauseInputStop = false;

        for (int i = 0; i < AttackInputs.Length; i++)
        {
            AttackInputs[i] = false;
        }
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeButton()
    {
        ResetInputActions();
        ResetInputVariables();
        isPaused = false;
        PauseInput = true;
        PauseInputStop = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;

        EventSystem.current.SetSelectedGameObject(null);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Reset the time scale to normal
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene (replace "MainMenu" with the actual name of your main menu scene)
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void OpenMemoriesMenu()
    {
        if (memoriesMenu != null)
        {
            pauseMenu.SetActive(false); // Hide the pause menu
            memoriesMenu.SetActive(true); // Show the memories menu
        }
        else
        {
            Debug.LogError("Memories Menu not assigned in PlayerInputHandler");
        }
    }

    public void OpenInventoryMenu()
{
    if (inventoryMenu != null)
    {
        pauseMenu.SetActive(false); // Hide the pause menu
        inventoryMenu.SetActive(true); // Show the inventory menu
    }
    else
    {
        Debug.LogError("Inventory Menu not assigned in PlayerInputHandler");
    }
}
}


public enum CombatInputs
{
    primary,
    secondary
}