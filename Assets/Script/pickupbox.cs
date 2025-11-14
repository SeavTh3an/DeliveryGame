using UnityEngine;

public class PickupBox : MonoBehaviour
{
    [Header("Delivery Settings")]
    public Transform deliveryPointParent; // Parent visuals
    public Transform deliveryPointChild;  // Child plane with trigger
    public LineRenderer deliveryLine;     // Optional line
    public Transform player;              // Player character picking up the box

    [HideInInspector]
    public bool carryingBox = false;

    private bool playerNearby = false;

    void Start()
    {
        // Hide delivery point and line at start
        if (deliveryPointParent != null)
            deliveryPointParent.gameObject.SetActive(false);

        if (deliveryLine != null)
            deliveryLine.enabled = false;
    }

    void Update()
    {
        // PICK UP BOX
        if (playerNearby && Input.GetKeyDown(KeyCode.E) && !carryingBox)
        {
            carryingBox = true;

            // Hide box
            GetComponent<Collider>().enabled = false;
            gameObject.SetActive(false);

            // Show delivery point visuals
            if (deliveryPointParent != null)
                deliveryPointParent.gameObject.SetActive(true);

            // Show line (optional)
            if (deliveryLine != null && player != null && deliveryPointChild != null)
            {
                deliveryLine.enabled = true;
                deliveryLine.SetPosition(0, player.position + Vector3.up * 0.1f);
                deliveryLine.SetPosition(1, deliveryPointChild.position + Vector3.up * 0.1f);
            }

            Debug.Log("📦 Box picked up! Drive to the delivery point.");
        }

        // UPDATE LINE FOLLOW (above car or player)
        if (carryingBox && deliveryLine != null && player != null && deliveryPointChild != null)
        {
            deliveryLine.SetPosition(0, player.position + Vector3.up * 0.1f);
            deliveryLine.SetPosition(1, deliveryPointChild.position + Vector3.up * 0.1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Player near box
        if (!carryingBox && other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player near box. Press E to pick up.");
        }

        // Car delivers box
        if (carryingBox && other.transform == deliveryPointChild)
        {
            // Check if the root object has tag "Car"
            if (other.attachedRigidbody != null && other.attachedRigidbody.transform.root.CompareTag("Car"))
            {
                carryingBox = false;

                // Hide line
                if (deliveryLine != null)
                    deliveryLine.enabled = false;

                // Hide delivery point
                if (deliveryPointParent != null)
                    deliveryPointParent.gameObject.SetActive(false);

                Debug.Log("🏆 Victory! Box delivered successfully by the car!");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!carryingBox && other.CompareTag("Player"))
            playerNearby = false;
    }
}
