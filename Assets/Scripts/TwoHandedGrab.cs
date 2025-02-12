using UnityEngine;
using UnityEngine.InputSystem;

public class TwoHandedGrab : MonoBehaviour
{
    public InputActionProperty grabAction; // Assign in Inspector
    public Transform otherHand; // Assign the opposite hand in Inspector

    private static Rigidbody grabbedObject; // Shared across both hands
    private static Transform grabbedTransform;
    private static TwoHandedGrab leftHandInstance, rightHandInstance;

    private bool isGrabbing;
    private static bool isLeftGrabbing, isRightGrabbing;
    private Vector3 grabOffset;
    private Quaternion grabRotationOffset;

    private void Awake()
    {
        if (gameObject.name.ToLower().Contains("left")) leftHandInstance = this;
        if (gameObject.name.ToLower().Contains("right")) rightHandInstance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable") && grabbedObject == null)
        {
            grabbedObject = other.GetComponent<Rigidbody>();
            grabbedTransform = grabbedObject.transform;
            Debug.Log("Grabbing area");
        }
    }

    private void Update()
    {
        bool gripPressed = grabAction.action.ReadValue<float>() > 0.5f;

        if (gripPressed && grabbedObject != null && !isGrabbing)
        {
            isGrabbing = true;
            grabbedObject.isKinematic = true; // Disable physics
            grabOffset = grabbedTransform.position - transform.position;
            grabRotationOffset = Quaternion.Inverse(transform.rotation) * grabbedTransform.rotation;
            Debug.Log("Grip pressed");
        }

        if (!gripPressed && isGrabbing)
        {
            isGrabbing = false;
            if (gameObject == leftHandInstance?.gameObject) isLeftGrabbing = false;
            if (gameObject == rightHandInstance?.gameObject) isRightGrabbing = false;
            if (!isLeftGrabbing && !isRightGrabbing && grabbedObject != null)
            {
                grabbedObject.isKinematic = false;
                grabbedObject = null;
            }
            Debug.Log("Grip pressed and grabbing");
        }

        if (isGrabbing && grabbedObject != null)
        {
            if (!isLeftGrabbing && gameObject == leftHandInstance?.gameObject) isLeftGrabbing = true;
            if (!isRightGrabbing && gameObject == rightHandInstance?.gameObject) isRightGrabbing = true;

            if (isLeftGrabbing && isRightGrabbing)
            {
                // Two-hand grabbing logic
                Vector3 midPoint = (leftHandInstance.transform.position + rightHandInstance.transform.position) / 2;
                grabbedTransform.position = midPoint;

                Quaternion leftRotation = leftHandInstance.transform.rotation * leftHandInstance.grabRotationOffset;
                Quaternion rightRotation = rightHandInstance.transform.rotation * rightHandInstance.grabRotationOffset;
                grabbedTransform.rotation = Quaternion.Slerp(leftRotation, rightRotation, 0.5f);
            }
            else
            {
                // Single-hand grabbing logic
                grabbedTransform.position = transform.position + grabOffset;
                grabbedTransform.rotation = transform.rotation * grabRotationOffset;
            }
        }
    }
}
