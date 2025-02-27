using UnityEngine;
using UnityEngine.InputSystem; // For VR input

public class MagneticHand : MonoBehaviour
{
    public InputActionReference gripButton; // Assign from inspector
    public InputActionReference triggerButton; // Assign from inspector for release
    private MagneticObject heldObject = null;

    private void Start()
    {
        gripButton.action.Enable();
        triggerButton.action.Enable(); // Enable the trigger button
    }

    void Update()
    {
        bool gripPressed = gripButton.action.IsPressed();
        bool triggerPressed = triggerButton.action.IsPressed(); // Check if trigger is pressed

        // Attach the object if the grip button is pressed and an object is detected
        if (gripPressed && heldObject != null)
        {
            heldObject.AttachToHand(transform);
            Debug.Log("Object attached: " + heldObject.name);
        }

        // Release the object if the trigger button is pressed
        if (triggerPressed && heldObject != null)
        {
            heldObject.Release(); // Call the release method in MagneticObject
            // Do not clear heldObject here, so it can be reattached
            Debug.Log("Object released: " + heldObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        MagneticObject obj = other.GetComponent<MagneticObject>();
        if (obj != null)
        {
            heldObject = obj; // Store the object when hand enters the collider
            Debug.Log("Trigger matched with: " + obj.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (heldObject != null && other.GetComponent<MagneticObject>() == heldObject)
        {
            heldObject = null; // Clear the held object when leaving the collider
            Debug.Log("Exited trigger with: " + other.name);
        }
    }
}
