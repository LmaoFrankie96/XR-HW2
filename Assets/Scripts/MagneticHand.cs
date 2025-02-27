using UnityEngine;
using UnityEngine.InputSystem; // For VR input

public class MagneticHand : MonoBehaviour
{
    public InputActionProperty gripButton; // Assign from inspector
    private MagneticObject heldObject = null;

    void Update()
    {
        bool gripPressed = gripButton.action.ReadValue<float>() > 0.1f;
        if (gripPressed)
        {
            Debug.Log("GripButton pressed");
        }
        if (gripPressed && heldObject != null)
        {
            heldObject.AttachToHand(transform);
            Debug.Log("GripButton pressed");
        }
        if (!gripPressed && heldObject != null)
        {
            heldObject.Release();
            heldObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MagneticObject obj = other.GetComponent<MagneticObject>();
        if (obj != null)
        {
            heldObject = obj; // Store the object when hand enters the collider
            Debug.Log("Trigger matched");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (heldObject != null && other.GetComponent<MagneticObject>() == heldObject)
        {
            heldObject = null; // Forget the object when leaving the collider
        }
    }
}
