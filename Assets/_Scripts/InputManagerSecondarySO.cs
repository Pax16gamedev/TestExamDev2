using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputManagerSecondarySO", menuName = "Scriptable Objects/InputManagerSecondarySO")]
public class InputManagerSecondarySO : ScriptableObject
{
    public static InputManagerSecondarySO Instance { get; private set; }

    private InputSystem_Actions controls;

    public event Action<Vector2> MovementAction;
    public event Action MovementCanceledAction;

    public bool IsRotateRightPressed => controls.Player.RotateRight.IsPressed();
    public bool IsRotateLeftPressed => controls.Player.RotateLeft.IsPressed();
    public bool IsIncrementScalePressed => controls.Player.IncrementSize.IsPressed();
    public bool IsDecreaseScalePressed => controls.Player.DecreaseSize.IsPressed();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        controls = new InputSystem_Actions();
        controls.Enable();

        controls.Player.Move.performed += OnMovementPerformedInput;
        controls.Player.Move.canceled += OnMovementCanceled;
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= OnMovementPerformedInput;
        controls.Player.Move.canceled -= OnMovementCanceled;

        controls.Disable();
    }

    private void OnMovementPerformedInput(InputAction.CallbackContext context)
    {
        var movement = context.ReadValue<Vector2>();
        MovementAction?.Invoke(movement);
    }

    private void OnMovementCanceled(InputAction.CallbackContext context)
    {
        MovementCanceledAction?.Invoke();
    }
}
