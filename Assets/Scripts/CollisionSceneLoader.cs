using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene loading

public class CollisionSceneLoader : MonoBehaviour
{
    public string sceneToLoad = "StartScene"; // Set your scene name in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the tag "Player"
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
