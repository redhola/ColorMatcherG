using UnityEngine;

public class playermovement : MonoBehaviour
{
    public float forwardSpeed = 5.0f;
    public float rotationSpeed = 200.0f;
    private Vector3 centerPoint;
    public float radius = 5.0f;
    public Transform cameraTransform;
    public Vector3 cameraOffset;
    public RectTransform backgroundImageTransform;
    public float touchSensitivity = 0.01f;
    private Renderer playerRenderer; 
    public float speedIncrement = 0.1f; 
    private float timeSinceLastIncrement = 0.0f;

    void Start()
    {
        centerPoint = transform.position + new Vector3(radius, 0, 0);
        playerRenderer = GetComponent<Renderer>();

        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        cameraOffset = cameraTransform.position - transform.position;
    }

    void Update()
    {
        MoveForward();

        timeSinceLastIncrement += Time.deltaTime;
        if (timeSinceLastIncrement >= 1f)
        {
            forwardSpeed += speedIncrement;
            timeSinceLastIncrement = 0f;
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            RotatePlayer(horizontalInput);
        }

        // mobile
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                float touchRotationInput = touch.deltaPosition.x * touchSensitivity;
                RotatePlayer(touchRotationInput);
            }
        }

        UpdateBackgroundAndCamera();
    }

    void LateUpdate()
    {
        cameraTransform.position = new Vector3(
            cameraTransform.position.x, 
            cameraTransform.position.y, 
            transform.position.z + cameraOffset.z
        );
    }

    void RotatePlayer(float input)
{
    transform.RotateAround(centerPoint, Vector3.back, input * rotationSpeed * Time.deltaTime);
}

void UpdateBackgroundAndCamera()
{
    if (backgroundImageTransform != null)
    {
        backgroundImageTransform.position = new Vector3(
            backgroundImageTransform.position.x, 
            backgroundImageTransform.position.y, 
            transform.position.z + 74
        );
    }

    cameraTransform.position = new Vector3(
        cameraTransform.position.x, 
        cameraTransform.position.y, 
        transform.position.z + cameraOffset.z
    );
}

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * forwardSpeed * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Ring ring = other.GetComponent<Ring>();
            if (ring != null)
            {
                Material newPlayerMaterial = ring.GetNextMaterial();
                if(newPlayerMaterial != null)
                {
                    playerRenderer.sharedMaterial = newPlayerMaterial;
                    GameManager.Instance.Gainscore(1);
                }
                else
                {
                    Debug.Log("No next material assigned in the ring.");
                    GameManager.Instance.GameOver();
                    forwardSpeed = 0;
                    rotationSpeed = 0;
                }
            }
        }
    }
}
