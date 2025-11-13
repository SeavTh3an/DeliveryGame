using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllerFPS : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform playerCamera;
    public float mouseSensitivity = 100f;

    [Header("Car Settings")]
    public GameObject car;          // car object
    public GameObject carCamera;    // third person car camera
    public float exitOffset = 2f;   // distance player appears from car

    private CharacterController controller;
    private Vector3 velocity;
    private float xRotation = 0f;
    private bool isDriving = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        car.GetComponent<CarController>().enabled = false;  // disable car at start
        carCamera.SetActive(false);
        playerCamera.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isDriving)
        {
            HandleMouseLook();
            HandleMovement();

            // Enter car
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(transform.position, car.transform.position) < 3f)
            {
                EnterCar();
            }
        }
        else
        {
            // Exit car
            if (Input.GetKeyDown(KeyCode.E))
            {
                ExitCar();
            }
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void HandleMovement()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -0.1f;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void EnterCar()
    {
        isDriving = true;

        // Hide player
        controller.enabled = false;
        SetPlayerVisible(false);

        // Switch cameras
        playerCamera.gameObject.SetActive(false);
        playerCamera.GetComponent<AudioListener>().enabled = false;
        carCamera.SetActive(true);
        carCamera.GetComponent<AudioListener>().enabled = true;

        // Enable car control
        car.GetComponent<CarController>().enabled = true;

        Debug.Log("🚗 Entered the car!");
    }

    void ExitCar()
    {
        isDriving = false;

        // Move player next to car
        transform.position = car.transform.position + car.transform.right * exitOffset;

        controller.enabled = true;
        SetPlayerVisible(true);

        // Switch cameras
        playerCamera.gameObject.SetActive(true);
        playerCamera.GetComponent<AudioListener>().enabled = true;
        carCamera.SetActive(false);
        car.GetComponent<CarController>().enabled = false;

        Debug.Log("🚶 Exited the car!");
    }

    void SetPlayerVisible(bool visible)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
            rend.enabled = visible;
    }
}
