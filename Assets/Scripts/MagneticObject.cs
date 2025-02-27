using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    private Transform attachedHand = null;
    private Rigidbody rb;

    public Vector3 attachOffset = new Vector3(-0.1f, 0, 0); // Adjust this value as needed
    public float repelForce = 5f; // Adjust the force applied when repelling

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (attachedHand != null)
        {
            // Move object to hand position with an offset
            transform.position = attachedHand.position + attachedHand.TransformDirection(attachOffset);
            transform.rotation = attachedHand.rotation;
        }
    }

    public void AttachToHand(Transform hand)
    {
        rb.isKinematic = true; // Disable physics while holding
        attachedHand = hand;
    }

    public void Release(Transform hand)
    {
        rb.isKinematic = false; // Re-enable physics so it stays in place

        // Always apply repelling force when releasing
        if (hand != null)
        {
            // Calculate direction away from the hand
            Vector3 direction = (transform.position - hand.position).normalized; // Direction away from hand
            rb.AddForce(direction * repelForce, ForceMode.Impulse); // Apply force to push away
        }

        attachedHand = null; // Reset the attached hand reference
    }
}
