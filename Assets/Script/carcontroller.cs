using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float speed = 10f;
    public float turnSpeed = 50f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * speed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed * Time.fixedDeltaTime;

        // Move forward/backward
        rb.MovePosition(rb.position + transform.forward * move * Time.fixedDeltaTime);

        // Rotate
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, turn, 0f));
    }
    
}
