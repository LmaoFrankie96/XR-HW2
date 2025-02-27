using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    private Transform attachedHand = null;
    private Rigidbody rb;

    // Define an offset for the object when attached
    public Vector3 attachOffset = new Vector3(-0.1f, 0, 0); // Adjust this value as needed

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

    public void Release()
    {
        rb.isKinematic = false; // Re-enable physics so it stays in place
        attachedHand = null;
    }
}
