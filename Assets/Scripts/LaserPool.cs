using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] int _poolSize = 10;
    [SerializeField] Transform _poolContainer;
    private List<GameObject> _poolObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject go = Instantiate(_laserPrefab, transform.position, Quaternion.identity, _poolContainer);
            go.SetActive(false);
            _poolObjects.Add(go);
        }
    }

    public void GetLaser(Vector3 spawnPOS)
    {
        foreach (GameObject laser in _poolObjects)
        {
            if (laser.activeInHierarchy == false)
            {
                laser.transform.position = spawnPOS;
                laser.SetActive(true);
                return;
            }
        }
        GameObject go = Instantiate(_laserPrefab, spawnPOS, Quaternion.identity, _poolContainer);
        _poolObjects.Add(go);
    }

}
