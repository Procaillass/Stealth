using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    #region Public Members
    public float m_rangeDetector;
    //public LayerMask m_coinLayer; // définir cela pour le calque contenant les coins
    #endregion

    #region Private Members
    [SerializeField]
    private List<GameObject> m_EnemiesToFind = new List<GameObject>();
    [SerializeField]
    private List<GameObject> m_EnemiesFind = new List<GameObject>();
    #endregion

    #region Unity API
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
            float distance = Vector3.Distance(transform.position, Enemy.transform.position);
            if (distance <= m_rangeDetector)
            {
                m_EnemiesFind.Add(Enemy);
                m_EnemiesToFind.RemoveAt(i);
                //Destroy(coin); 
                Enemy.SetActive(false);
                Debug.Log(Enemy.name + "C'EST LA GUERRE");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, m_rangeDetector);
    }
    #endregion
}
