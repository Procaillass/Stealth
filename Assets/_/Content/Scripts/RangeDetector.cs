// RangeDetector.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    public float m_rangeDetector;
    public GameObject explosionPrefab;

    private List<GameObject> m_EnemiesToFind = new List<GameObject>();
    private List<GameObject> m_EnemiesFind = new List<GameObject>();

    private void Awake()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject Enemy in allEnemies)
        {
            m_EnemiesToFind.Add(Enemy);
        }
    }

    private void FixedUpdate()
    {
        for (int i = m_EnemiesToFind.Count - 1; i >= 0; i--)
        {
            GameObject Enemy = m_EnemiesToFind[i];

            if (Enemy == null)
            {
                m_EnemiesToFind.RemoveAt(i);
                continue;
            }


            float distance = Vector3.Distance(transform.position, Enemy.transform.position);
            if (distance <= m_rangeDetector)
            {
                Enemy.SetActive(false);

                m_EnemiesFind.Add(Enemy);
                m_EnemiesToFind.RemoveAt(i);
                Debug.Log(Enemy.name + "C'EST LA GUERRE");

                GameObject explosionInstance = Instantiate(explosionPrefab, Enemy.transform.position, Quaternion.identity);
                if (explosionInstance != null)
                {
                    Destroy(explosionInstance, 1f);
                }

                
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_rangeDetector);
    }
}
