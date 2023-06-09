using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocatorIdentity : MonoBehaviour
{
    #region Public Members

    public LocatorSystem m_locatorSystem;
    public List<GameObject> m_locatorList;

    #endregion


    #region Private and Protected Members

    [SerializeField]
    private string locatorPrefix = "Locator";
    private NavMeshAgent _navmeshAgent;
    private Vector3 _currentDestination;    

    #endregion


    #region Unity API
    private void OnEnable()
    {
       

        m_locatorList = GetLocatorsInScene();
    }

    private void Start()
    {
        m_locatorSystem = FindObjectOfType<LocatorSystem>();

        if (!TryGetComponent(out _navmeshAgent))
        {
            Debug.LogError("NavMeshAgent not found");
            return;
        }

        MoveToLocation();
    }

    private void Update()
    {
        if (HasReachedDestination())
        {
             MoveToLocation();
        }
    }

    #endregion


    #region Main Methods
    public void MoveToLocation()
    {
        _currentDestination = m_locatorSystem.GetRandom();
        _navmeshAgent.SetDestination(_currentDestination);
    }

    public void MoveToNextLocation()
    {
        _currentDestination = m_locatorSystem.GetNext();
        _navmeshAgent.SetDestination(_currentDestination);
    }
    #endregion


    #region Utils
    private List<GameObject> GetLocatorsInScene()
    {
        List<GameObject> locators = new List<GameObject>();
        int i = 1;
        while (true)
        {
            GameObject locator = GameObject.Find(locatorPrefix + i.ToString());
            if (locator != null)
            {
                locators.Add(locator);
                i++;
            }
            else
            {
                break;
            }
        }

        return locators;
    }

    private bool HasReachedDestination()
    {
        return Vector3.Distance(transform.position, _currentDestination) < 1;
    }
    #endregion
   

}
