using UnityEngine;

public class FreeCameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 20f;
    public float zoomSpeed = 500f;
    public float rotationSpeed = 3f;

    private float pitch = 0f;
    private float yaw = 0f;
    
    void Start()
    {
        // Initialize rotation to match current camera view
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;
    }

    
    void Update()
    {
        HandleRotation();
        HandleMovement();
        HandleZoom();
    }

    private void HandleRotation()
    {
        // Rotate Only when right mouse button is held
        if(Input.GetMouseButton(1))
        {
            yaw += Input.GetAxis("Mouse X") * rotationSpeed;
            pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -85f, 85f);
            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }

    private void HandleMovement()
    {
        Vector3 moveInput = Vector3.zero;

        if(Input.GetKey(KeyCode.W)) moveInput += transform.forward;
        if(Input.GetKey(KeyCode.S)) moveInput -= transform.forward;
        if(Input.GetKey(KeyCode.A)) moveInput -= transform.right;
        if(Input.GetKey(KeyCode.D)) moveInput += transform.right;

        if(moveInput != Vector3.zero)
        {
            transform.position += moveInput.normalized * moveSpeed * Time.unscaledDeltaTime;
        }
    }

    private void HandleZoom()
    {
        // Zoom with Scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * scroll * zoomSpeed * Time.unscaledDeltaTime;
    }
}
