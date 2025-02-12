using UnityEngine;

public class TwoHandedGrab : MonoBehaviour
{
    public Transform leftHand, rightHand; // Assign in Inspector
    private Rigidbody grabbedObject;
    private Transform grabbedTransform;

    private bool isLeftGrabbing, isRightGrabbing;
    private Vector3 leftGrabOffset, rightGrabOffset;
    private Quaternion leftGrabRotationOffset, rightGrabRotationOffset;

    public bool enableDoubleRotation = false; // Toggle for extra credit feature

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable") && grabbedObject == null)
        {
            grabbedObject = other.GetComponent<Rigidbody>();
            grabbedTransform = grabbedObject.transform;
            grabbedObject.isKinematic = true; // Prevent physics from interfering
        }

        if (other.gameObject == leftHand.gameObject)
        {
            isLeftGrabbing = true;
            leftGrabOffset = grabbedTransform.position - leftHand.position;
            leftGrabRotationOffset = Quaternion.Inverse(leftHand.rotation) * grabbedTransform.rotation;
        }
        else if (other.gameObject == rightHand.gameObject)
        {
            isRightGrabbing = true;
            rightGrabOffset = grabbedTransform.position - rightHand.position;
            rightGrabRotationOffset = Quaternion.Inverse(rightHand.rotation) * grabbedTransform.rotation;
        }
    }

    private void Update()
    {
        if (grabbedObject != null)
        {
            if (isLeftGrabbing && isRightGrabbing)
            {
                // Compute new position: Midpoint between both hands
                grabbedTransform.position = (leftHand.position + rightHand.position) / 2;

                // Compute combined rotation
                Quaternion leftRotation = leftHand.rotation * leftGrabRotationOffset;
                Quaternion rightRotation = rightHand.rotation * rightGrabRotationOffset;

                Quaternion deltaRotation = rightRotation * Quaternion.Inverse(leftRotation);

                if (enableDoubleRotation)
                {
                    // Double the rotation effect
                    grabbedTransform.rotation = Quaternion.Slerp(Quaternion.identity, deltaRotation, 2f) * grabbedTransform.rotation;
                }
                else
                {
                    grabbedTransform.rotation = deltaRotation * grabbedTransform.rotation;
                }
            }
            else if (isLeftGrabbing)
            {
                grabbedTransform.position = leftHand.position + leftGrabOffset;
                grabbedTransform.rotation = leftHand.rotation * leftGrabRotationOffset;
            }
            else if (isRightGrabbing)
            {
                grabbedTransform.position = rightHand.position + rightGrabOffset;
                grabbedTransform.rotation = rightHand.rotation * rightGrabRotationOffset;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == leftHand.gameObject)
            isLeftGrabbing = false;
        if (other.gameObject == rightHand.gameObject)
            isRightGrabbing = false;

        if (!isLeftGrabbing && !isRightGrabbing && grabbedObject != null)
        {
            grabbedObject.isKinematic = false; // Re-enable physics when released
            grabbedObject = null;
        }
    }
}
