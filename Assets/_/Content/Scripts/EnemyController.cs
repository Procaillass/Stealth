using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float detectionRange = 10.0f;
    public GameObject player;
    public LayerMask viewMask; // layers the enemy can see
    public LineRenderer lineRenderer;

    private bool playerInSight;

    private void Awake()
    {
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
        }

        lineRenderer.positionCount = 2;
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRange, viewMask);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == player)
            {
                Vector3 directionToPlayer = player.transform.position - transform.position;
                if (!Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, detectionRange, viewMask) || hit.collider.gameObject == player)
                {
                    playerInSight = true;

                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, player.transform.position);
                    break;
                }
            }
        }

        if (playerInSight)
        {
            // Rotate towards player
            Vector3 direction = player.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime);

            // On detection, trigger the explosion
            OnDetect();
        }
        else
        {
            // Clear line renderer
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, Vector3.zero);
        }
    }

    private bool hasExploded = false;

    public void OnDetect()
    {
        if (!hasExploded)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
            Destroy(explosion, 5f);
            hasExploded = true;
            Destroy(this.gameObject, 5f); // Ajoutez cette ligne pour détruire l'ennemi
        }
    }

}
