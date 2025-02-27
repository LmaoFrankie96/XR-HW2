using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
    public float speed = 3f; // Speed of the object
    private MagneticObject magneticObject; // Reference to MagneticObject component
  
    private void Start()
    {
        magneticObject = GetComponent<MagneticObject>();
        
    }

    private void Update()
    {
        
            // Move towards the player only if the flag is true
            if (magneticObject.shouldMoveTowardsPlayer)
            {
                transform.position = Vector3.MoveTowards(transform.position, Camera.main.transform.position, speed * Time.deltaTime);
            }
        
    }
}
