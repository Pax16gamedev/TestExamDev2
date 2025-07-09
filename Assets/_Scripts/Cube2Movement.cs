using UnityEngine;

public class Cube2Movement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 12f;
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float scalingSpeed = 0.5f;
    [SerializeField] private float minScale = 0.1f;

    [SerializeField] private InputManagerSecondarySO inputSO;

    private Vector2 currentInput;
    private Vector3 currentScale;

    private void Awake()
    {
        currentScale = transform.localScale;

        if (inputSO == null)
            inputSO = InputManagerSecondarySO.Instance;
    }

    private void OnEnable()
    {
        // Suscribirse al movimiento
        inputSO.MovementAction += SetMovementInput;
        inputSO.MovementCanceledAction += ClearMovementInput;
    }

    private void OnDisable()
    {
        inputSO.MovementAction -= SetMovementInput;
        inputSO.MovementCanceledAction -= ClearMovementInput;
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleScaling();
    }

    void SetMovementInput(Vector2 movement)
    {
        currentInput = movement;
    }

    void ClearMovementInput()
    {
        currentInput = Vector2.zero;
    }

    void HandleMovement()
    {
        Vector3 direction = currentInput.x * Vector3.right + currentInput.y * Vector3.up;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (inputSO.IsRotateRightPressed)
        {
            transform.Rotate(Vector3.forward, -rotationAmount);
        }
        else if (inputSO.IsRotateLeftPressed)
        {
            transform.Rotate(Vector3.forward, rotationAmount);
        }
    }

    void HandleScaling()
    {
        float scaleChange = scalingSpeed * Time.deltaTime;
        Vector3 newScale = transform.localScale;

        if (inputSO.IsIncrementScalePressed)
        {
            newScale += Vector3.one * scaleChange;
        }
        else if (inputSO.IsDecreaseScalePressed)
        {
            newScale -= Vector3.one * scaleChange;
        }

        newScale.x = Mathf.Max(minScale, newScale.x);
        newScale.y = Mathf.Max(minScale, newScale.y);
        newScale.z = Mathf.Max(minScale, newScale.z);

        transform.localScale = newScale;
    }
}
