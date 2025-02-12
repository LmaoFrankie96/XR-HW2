using UnityEngine;

public class DynamicCullingMask : MonoBehaviour
{
    public Camera mainCamera; // Reference to the main camera (VR camera)
    public LayerMask magnifyingGlassLayer; // Layer for the magnifying glass
    public LayerMask hiddenLayer; // Layer for the hidden object
   

    private bool isLookingThroughMagnifyingGlass = false;

    void Update()
    {
        // Create a ray from the center of the main camera
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        // Check if the ray hits the magnifying glass
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, magnifyingGlassLayer))
        {
            // If the ray hits the magnifying glass, include the HiddenLayer in the culling mask
            if (!isLookingThroughMagnifyingGlass)
            {
                mainCamera.cullingMask |= hiddenLayer;// Include HiddenLayer
                isLookingThroughMagnifyingGlass = true;
            }
        }
        else
        {
            // If the ray does not hit the magnifying glass, exclude the HiddenLayer from the culling mask
            if (isLookingThroughMagnifyingGlass)
            {
                mainCamera.cullingMask &= ~hiddenLayer; // Exclude HiddenLayer
                isLookingThroughMagnifyingGlass = false;
            }
        }
    }
}