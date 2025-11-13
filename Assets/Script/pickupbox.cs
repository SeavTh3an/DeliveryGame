using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PickupBox : MonoBehaviour
{
    private bool playerNearby = false;

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Pick up or remove the box
            gameObject.SetActive(false);
            Debug.Log("📦 Box picked up!");
        }
    }

    // Detect when player enters trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player is near the box!");  // collision detected
        }
    }

    // Detect when player leaves trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            Debug.Log("Player left the box!");
        }
    }
}
