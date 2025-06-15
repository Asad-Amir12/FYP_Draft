using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MouseDelta;
    public Vector2 MoveComposite;

    public Action OnJumpPerformed;

    public Action OnRollPerformed;
    public Action OnMenuControlsPerformed;
    public Action OnAttackPerformed;
    private Controls controls;

    public bool GamePaused = false;

    private void OnEnable()
    {
        if (controls != null)
            return;

        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    public void OnDisable()
    {

        controls.Player.Disable();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        MouseDelta = context.ReadValue<Vector2>() * 5;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveComposite = context.ReadValue<Vector2>();
    }

    bool pressed_once = false;
    float firstPressTime = 0f;
    float delayBetweenPresses = 0.25f;
    float lastPressdedTime = 0f;
    bool roll = false;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;


        if (context.performed)
        {
            roll = false;
            Invoke(nameof(resetPress), 0.25f);
            if (!pressed_once)
            {
                pressed_once = true;
                firstPressTime = Time.time;
            }
            else if (pressed_once)
            {
                bool isDoublePress = Time.time - lastPressdedTime <= delayBetweenPresses;

                if (isDoublePress)
                {
                    pressed_once = false;
                    OnRollPerformed?.Invoke();
                    roll = true;
                }
            }
            lastPressdedTime = Time.time;
        }
    }

    private void resetPress()
    {
        pressed_once = false;
        // Debug.Log("resetting the bool counter");
        if (!roll)
        {
            OnJumpPerformed?.Invoke();
        }
    }
    public void OnRoll(InputAction.CallbackContext context)
    {
        // if(!context.performed)
        //     return;

        // OnRollPerformed?.Invoke();
    }

    public void OnMenuControls(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        Debug.Log("Menu Controls");
        GamePaused = !GamePaused;
        OnMenuControlsPerformed?.Invoke();

    }

    public void DisableControls()
    {

        controls.Player.Move.Disable();
        controls.Player.Look.Disable();
        controls.Player.Roll.Disable();
        controls.Player.Attack.Disable();
        controls.Player.Look.Disable();
        controls.Player.Jump.Disable();


    }
    public void EnableControls()
    {

        controls.Player.Move.Enable();
        controls.Player.Look.Enable();
        controls.Player.Roll.Enable();
        controls.Player.Attack.Enable();
        controls.Player.Look.Enable();
        controls.Player.Jump.Enable();

    }

    public void ToggleControls(bool state)
    {
        if (state)
        {
            EnableControls();
        }
        else
        {
            DisableControls();
        }
    }



    float firstClickTime = 0f;
    float delayBetweenClicks = 0.3f;
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;
        if (Time.time > firstClickTime + delayBetweenClicks)
        {
            OnAttackPerformed?.Invoke();
            Debug.Log("Attack performed");
            lastPressdedTime = Time.time;

        }

        // Handle attack input here
    }
}