using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarPushNPC : MonoBehaviour
{
    public float pushForce = 2000f;   // Stronger horizontal push
    public float upwardForce = 500f;  // Vertical bounce

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("NPC"))
        {
            Rigidbody npcRb = collision.collider.attachedRigidbody;
            if (npcRb != null)
            {
                // Direction from car to NPC
                Vector3 pushDirection = (collision.collider.transform.position - transform.position).normalized;

                // Apply a strong impulse to push NPC farther
                Vector3 force = pushDirection * pushForce + Vector3.up * upwardForce;
                npcRb.AddForce(force, ForceMode.Impulse);
            }
        }
    }
}
