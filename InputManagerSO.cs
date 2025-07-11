using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManagerSO", menuName = "Scriptable Objects/InputManagerSO")]
public class InputManagerSO : ScriptableObject
{
    public static InputManagerSO Instance;

    InputSystem_Actions controls;

    public event Action<Vector2> MovementInput;
    public event Action MovementCanceledInput;

    public event Action SpecialAttackInput;

    public bool IsRotateRightPressed => controls.Player.RotateRight.IsPressed();
    public bool IsRotateLeftPressed => controls.Player.RotateLeft.IsPressed();
    public bool IsScalingUpPressed => controls.Player.ScaleUp.IsPressed();
    public bool IsScalingDownPressed => controls.Player.ScaleDown.IsPressed();

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        controls = new();

        controls.Player.Move.started += OnMovementStarted;
        controls.Player.Move.canceled += OnMovementCanceled;

        controls.Player.SpecialAttack.started += SpecialAttack_Started;

        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();

        controls.Player.Move.started -= OnMovementStarted;
        controls.Player.Move.canceled -= OnMovementCanceled;

        controls.Player.SpecialAttack.started -= SpecialAttack_Started;
    }

    private void OnMovementStarted(InputAction.CallbackContext context)
    {
        var movementInput = context.ReadValue<Vector2>().normalized;
        MovementInput?.Invoke(movementInput);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        MovementCanceledInput?.Invoke();
    }

    private void SpecialAttack_Started(InputAction.CallbackContext obj)
    {
        SpecialAttackInput?.Invoke();
    }
}
