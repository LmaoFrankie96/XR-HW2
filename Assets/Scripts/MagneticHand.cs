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

        if (gripPressed && heldObject != null)
        {
            heldObject.AttachToHand(transform);
        }

        // Check if the trigger button is pressed for releasing
        bool triggerPressed = triggerButton.action.IsPressed(); // Assuming you have an InputActionReference for the trigger button

        if (triggerPressed && heldObject != null)
        {
            heldObject.Release(transform); // Pass the current hand transform
            heldObject = null; // Reset held object reference
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
