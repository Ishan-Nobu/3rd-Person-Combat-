using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{   
    public event Action JumpEvent;
    public event Action DodgeEvent;
    private Controls controls;
    private void Awake()
    {
        controls =  new Controls();
        controls.Player.SetCallbacks(this);  
    }
    private void OnEnable()
    {
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {   
        if (!context.performed) return;
        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        DodgeEvent?.Invoke();
    }
}
