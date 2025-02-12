using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CustomGrab : MonoBehaviour
{

    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    public InputActionReference action2;
    public TextMeshProUGUI doubleRotationText;
    bool grabbing = false;

    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private bool doubleRotationEnabled = false; // Extra credit feature

    private void Start()
    {
        action.action.Enable();
        if (action2 != null)
            action2.action.Enable();

        // Find the other hand
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }

        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (action2 != null)
        {
            action2.action.performed += (ctx) =>
            {


                ToggleDoubleRotation();
                if (doubleRotationEnabled == true)
                    doubleRotationText.text = "ON";
                if (doubleRotationEnabled == false)
                    doubleRotationText.text = "OFF";
            };
        }
        if (grabbing)
        {
            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;

            if (grabbedObject)
            {
                // Calculate delta position and rotation
                Vector3 deltaPosition = transform.position - previousPosition;
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                // Apply delta position and rotation to the grabbed object
                if (otherHand != null && otherHand.grabbedObject == grabbedObject)
                {
                    // If both hands are grabbing the same object, combine the deltas
                    Vector3 otherDeltaPosition = otherHand.transform.position - otherHand.previousPosition;
                    Quaternion otherDeltaRotation = otherHand.transform.rotation * Quaternion.Inverse(otherHand.previousRotation);

                    deltaPosition += otherDeltaPosition;
                    deltaRotation *= otherDeltaRotation;
                }

                // Apply the combined deltas to the grabbed object
                grabbedObject.position += deltaPosition;
                grabbedObject.rotation = deltaRotation * grabbedObject.rotation;

                // Extra credit: Double the rotation
                if (doubleRotationEnabled)
                {
                    grabbedObject.rotation = Quaternion.SlerpUnclamped(Quaternion.identity, deltaRotation, 2.0f) * grabbedObject.rotation;
                }
            }
        }
        // If let go of button, release object
        else if (grabbedObject)
            grabbedObject = null;

        // Save the current position and rotation for the next frame
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {


        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Add(t);
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
            nearObjects.Remove(t);
    }

    // Extra credit: Toggle double rotation
    public void ToggleDoubleRotation()
    {
        doubleRotationEnabled = !doubleRotationEnabled;
    }
}