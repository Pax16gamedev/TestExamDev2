using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    private PlayerInput playerInput;

    public Vector2 MovementInput {  get; private set; }
    public bool RotationRightInput { get; private set; }
    public bool RotationLeftInput { get; private set; }
    public bool IncrementScaleInput { get; private set; }
    public bool DecreaseScaleInput { get; private set; }


    private InputAction movementAction;

    private InputAction rotationRightAction;
    private InputAction rotationLeftAction;

    private InputAction incrementScaleAction;
    private InputAction decreaseScaleAction;

    [Header("Movement")]
    [SerializeField] private InputActionReference movementActionReference;

    [Header("Rotation")]
    [SerializeField] private InputActionReference rotationRightActionReference;
    [SerializeField] private InputActionReference rotationLeftActionReference;

    [Header("Scaling")]
    [SerializeField] private InputActionReference incrementScaleActionReference;
    [SerializeField] private InputActionReference decreaseScaleActionReference;


    private void Awake()
    {
        SetSingleton();

        playerInput = GetComponent<PlayerInput>();
    }

    private void SetSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movementAction = movementActionReference.action;

        rotationRightAction = rotationRightActionReference.action;
        rotationLeftAction = rotationLeftActionReference.action;

        incrementScaleAction = incrementScaleActionReference.action;
        decreaseScaleAction = decreaseScaleActionReference.action;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInputs();
    }

    void HandleInputs()
    {
        MovementInput = movementAction.ReadValue<Vector2>();

        RotationRightInput = rotationRightAction.IsPressed();
        RotationLeftInput = rotationLeftAction.IsPressed();

        IncrementScaleInput = incrementScaleAction.IsPressed();
        DecreaseScaleInput = decreaseScaleAction.IsPressed();
    }
}
