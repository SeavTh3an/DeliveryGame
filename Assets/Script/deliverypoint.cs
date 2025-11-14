using UnityEngine;

public class DeliveryPointTrigger : MonoBehaviour
{
    public PickupBox pickupBox; // Assign the PickupBox in inspector
    public LineRenderer deliveryLine;
    public GameObject deliveryPointParent;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") && pickupBox != null && pickupBox.carryingBox)
        {
            // Victory!
            pickupBox.carryingBox = false;

            if (deliveryLine != null)
                deliveryLine.enabled = false;

            if (deliveryPointParent != null)
                deliveryPointParent.SetActive(false);

            Debug.Log("🏆 Victory! Box delivered successfully by the car!");
        }
    }
}
