using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        // check if the object we collided with is the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Instantiate the explosion prefab at this position and rotation
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);

            // Optional: destroy the explosion effect after some time
            Destroy(explosion, 5f);
        }
    }
}