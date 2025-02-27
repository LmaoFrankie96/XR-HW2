using UnityEngine;

public class MagneticObject : MonoBehaviour
{
    private Transform attachedHand = null;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (attachedHand != null)
        {
            // Move object to hand position
            transform.position = attachedHand.position;
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
