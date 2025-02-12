using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CustomGrab : MonoBehaviour
{
    public Transform grabPoint; // Set this to the empty GrabPoint object
    private Rigidbody grabbedObject;
    private Transform grabbedTransform;
    private Vector3 grabOffset;
    private Quaternion grabRotationOffset;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable") && grabbedObject == null)
        {
            grabbedObject = other.GetComponent<Rigidbody>();
            grabbedTransform = grabbedObject.transform;
            grabOffset = grabbedTransform.position - grabPoint.position;
            grabRotationOffset = Quaternion.Inverse(grabPoint.rotation) * grabbedTransform.rotation;
        }
    }

    private void Update()
    {
        if (grabbedObject != null)
        {
            grabbedTransform.position = grabPoint.position + grabOffset;
            grabbedTransform.rotation = grabPoint.rotation * grabRotationOffset;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grabbable") && grabbedObject != null)
        {
            grabbedObject = null;
        }
    }
}
