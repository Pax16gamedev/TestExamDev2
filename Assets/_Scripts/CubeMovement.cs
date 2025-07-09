using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 12;
    [SerializeField] private float rotationSpeed = 90;
    [SerializeField] private float scalingSpeed = 0.5f;
    [SerializeField] private float minScale = 0.1f;

    private Rigidbody rb;
    private Vector2 currentInput;
    private Vector2 direction;
    private Vector3 currentScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleSizeScaling();
    }

    void HandleMovement()
    {
        currentInput = InputManager.Instance.MovementInput.normalized;
        direction = currentInput.x * Vector3.right + currentInput.y * Vector3.up;

        transform.position += (Vector3) direction * movementSpeed * Time.deltaTime;
    }

    void HandleRotation()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;

        if (InputManager.Instance.RotationRightInput)
        {
            transform.Rotate(Vector3.forward, -rotationAmount); // Rotar a la derecha en eje Z negativo
        }
        else if (InputManager.Instance.RotationLeftInput)
        {
            transform.Rotate(Vector3.forward, rotationAmount); // Rotar a la izquierda en eje Z positivo
        }
    }

    void HandleSizeScaling()
    {
        float scaleChange = scalingSpeed * Time.deltaTime;
        Vector3 newScale = transform.localScale;

        if (InputManager.Instance.IncrementScaleInput)
        {
            newScale += Vector3.one * scaleChange;
        }
        else if (InputManager.Instance.DecreaseScaleInput)
        {
            newScale -= Vector3.one * scaleChange;
        }

        // Clampear el tamaño para evitar que se haga demasiado grande o desaparezca
        newScale.x = Mathf.Clamp(newScale.x, minScale, Mathf.Infinity);
        newScale.y = Mathf.Clamp(newScale.y, minScale, Mathf.Infinity);
        newScale.z = Mathf.Clamp(newScale.z, minScale, Mathf.Infinity);

        transform.localScale = newScale;
    }
}
