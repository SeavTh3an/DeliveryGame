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
        rb.useGravity = true;     // Make sure gravity is enabled
        rb.isKinematic = false;   // Must be non-kinematic
        rb.mass = 1200f;          // More realistic weight
    }

    void FixedUpdate()
    {
        float move = Input.GetAxis("Vertical") * speed;
        float turn = Input.GetAxis("Horizontal") * turnSpeed;

        // Move using velocity (keeps gravity)
        Vector3 forwardVelocity = transform.forward * move;
        rb.velocity = new Vector3(forwardVelocity.x, rb.velocity.y, forwardVelocity.z);

        // Rotate car
        transform.Rotate(Vector3.up * turn * Time.fixedDeltaTime);
    }
}
