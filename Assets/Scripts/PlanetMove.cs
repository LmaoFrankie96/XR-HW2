using UnityEngine;

public class PlanetMove : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the inspector
    public float duration = 10f; // Time in seconds to reach the player
    private bool isMoving = false;
    private Vector3 startPosition; // Initial position of the object
    private Vector3 targetPosition; // Target position (player's position)
    private float startTime; // Time when movement starts
   // private MagneticObject magneticObject; // Reference to MagneticObject component
    private void Start()
    {
       // magneticObject = GetComponent<MagneticObject>();
        if (player == null)
        {
            player = Camera.main.transform; // Default to the main camera if player is not set
        }
        StartMoving();
    }

    private void Update()
    {
        

            if (MagneticObject.winCondition == false)
            {
                if (isMoving)
                {
                    MoveTowardsPlayer();
                }
            }
        
        
    }

    public void StartMoving()
    {
        startPosition = transform.position; // Store the initial position
        targetPosition = player.position; // Set the target position to the player's position
        startTime = Time.time; // Record the time when movement starts
        isMoving = true; // Start the movement
    }

    private void MoveTowardsPlayer()
    {
        float elapsedTime = Time.time - startTime; // Calculate the time elapsed since movement started

        // Calculate the fraction of time completed
        float fraction = elapsedTime / duration;

        // Move the object towards the player
        transform.position = Vector3.Lerp(startPosition, targetPosition, fraction);

        // Stop moving once the time duration is complete
        if (fraction >= 1f)
        {
            isMoving = false; // Stop the movement
        }
    }
}
