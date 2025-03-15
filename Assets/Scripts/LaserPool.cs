using System.Collections.Generic;
using UnityEngine;

public class LaserPool : MonoBehaviour
{
    [SerializeField] GameObject _laserPrefab;
    [SerializeField] GameObject _wavePrefab;
    [SerializeField] GameObject _tripleshotPrefab;
    [SerializeField] int _poolSize = 10;
    [SerializeField] Transform _poolContainer;
    private List<GameObject> _poolLaserObjects = new List<GameObject>();
    private List<GameObject> _poolWaveObjects = new List<GameObject>();
    private List<GameObject> _poolTripleShotObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            //Spawn Laser into Pool
            GameObject laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity, _poolContainer);
            laser.SetActive(false);
            _poolLaserObjects.Add(laser);
        }

        for (int i = 0; i < _poolSize; i++)
        {
            //Spawn Wave into Pool
            GameObject wave = Instantiate(_wavePrefab, transform.position, Quaternion.identity, _poolContainer);
            wave.SetActive(false);
            _poolWaveObjects.Add(wave);
        }

        for (int i=0; i < _poolSize; i++)
        { 
            //Spawn Tripleshot into Pool
            GameObject tripleshot = Instantiate(_tripleshotPrefab, transform.position, Quaternion.identity, _poolContainer);
            tripleshot.SetActive(false);
            _poolTripleShotObjects.Add(tripleshot);
        }
    }

    public void GetLaser(Vector3 spawnPOS)
    {
        foreach (GameObject laser in _poolLaserObjects)
        {
            if (laser.activeInHierarchy == false)
            {
                laser.transform.position = spawnPOS;
                laser.SetActive(true);
                return;
            }
        }
        GameObject go = Instantiate(_laserPrefab, spawnPOS, Quaternion.identity, _poolContainer);
        _poolLaserObjects.Add(go);
    }

    public void GetWave(Vector3 spawnPOS)
    {
        foreach(GameObject wave in _poolWaveObjects)
        {
            if (wave.activeInHierarchy == false)
            {
                wave.transform.position = spawnPOS;
                wave.SetActive(true);
                return;
            }
        }
        GameObject go = Instantiate(_wavePrefab, spawnPOS, Quaternion.identity, _poolContainer);
        _poolWaveObjects.Add(go);
    }

    public void GetTripleShot(Vector3 spawnPOS)
    {
        foreach (GameObject tripleshot in _poolTripleShotObjects)
        {
            if (!tripleshot.activeInHierarchy)
            {
                tripleshot.transform.position = spawnPOS;
                tripleshot.SetActive(true);
                
                for(int i = 0; i<tripleshot.transform.childCount; i++)
                {
                    tripleshot.transform.GetChild(i).gameObject.SetActive(true);
                }
                return;
            }
        }
        GameObject go = Instantiate(_tripleshotPrefab, spawnPOS, Quaternion .identity, _poolContainer);
        _poolTripleShotObjects.Add(go);
    }

}
