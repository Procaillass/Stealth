using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocatorSystem : MonoBehaviour
{


    #region Public Members
    public LocatorIdentity m_locatorIdentity;
    #endregion


    #region Private Members
    private int _currentIndex = 0;
    #endregion


    #region Main Methods
    public Vector3 GetRandom()
    {
        if (CheckLocatorListEmpty()) return m_locatorIdentity.transform.position;

        _currentIndex = UnityEngine.Random.Range(0, m_locatorIdentity.m_locatorList.Count);
        return GetLocatorPosition();
    }

    public Vector3 GetNext()
    {
        if (CheckLocatorListEmpty()) return m_locatorIdentity.transform.position;

        _currentIndex = (_currentIndex + 1) % m_locatorIdentity.m_locatorList.Count;
        return GetLocatorPosition();
    }

    public Vector3 GetPrevious()
    {
        if (CheckLocatorListEmpty()) return m_locatorIdentity.transform.position;

        _currentIndex--;
        if (_currentIndex < 0)
            _currentIndex = m_locatorIdentity.m_locatorList.Count - 1;

        return GetLocatorPosition();
    }
    #endregion


    #region Utils
    private bool CheckLocatorListEmpty()
    {
        return m_locatorIdentity.m_locatorList.Count == 0;
    }

    private Vector3 GetLocatorPosition()
    {
        return m_locatorIdentity.m_locatorList[_currentIndex].transform.position;
    }
    #endregion


}
