using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputManager")]
public class InputManagerSO : ScriptableObject
{
    public static InputManagerSO Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }

    InputSystem_Actions controls;

    public event Action<Vector2> OnMovement;
    public event Action OnMovementCanceled;
    public event Action OnJump;

    // En cuanto el editor se inicialize, este manager tambien
    private void OnEnable()
    {
        controls = new();
        controls.Player.Enable(); // No pueden convivir mas de 1 input action
        controls.Player.Jump.started += OnJumpStarted;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Move.performed += OnMovePerformed;
    }

    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        OnMovementCanceled?.Invoke();
    }

    private void OnDisable()
    {
        controls.Player.Jump.started -= OnJumpStarted;
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Disable();
    }

    // Solo se actualiza cuando hay movimiento
    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        var movementValue = ctx.ReadValue<Vector2>();
        OnMovement?.Invoke(movementValue);
    }

    private void OnJumpStarted(InputAction.CallbackContext ctx)
    {
        OnJump?.Invoke();
    }


}