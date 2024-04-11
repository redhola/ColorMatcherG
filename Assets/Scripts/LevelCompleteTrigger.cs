using UnityEngine;

public class LevelCompleteTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Make sure the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Call the LevelComplete method on the GameManager
            GameManager.Instance.LevelComplete();
        }
    }
}