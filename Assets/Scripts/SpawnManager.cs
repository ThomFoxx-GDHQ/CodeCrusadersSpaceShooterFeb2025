using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    private float _randomNumber;
    private Vector3 _spawnPOS = Vector3.zero;
    private WaitForSeconds _spawnDelayTimer;
    [SerializeField] private float _spawnTime;
    private bool _isSpawning = true;
    [SerializeField] float _powerupSpawnChance = .5f;
    [SerializeField] private GameObject[] _powerupPrefabs;

    private void Start()
    {
        _spawnDelayTimer = new WaitForSeconds(_spawnTime);
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        while (_isSpawning == true)
        {
            _randomNumber = Random.Range(-5, 5);
            _spawnPOS.y = _randomNumber;
            _spawnPOS.x = 15;
            Instantiate(_enemyPrefab, _spawnPOS, Quaternion.identity);
            yield return _spawnDelayTimer;
        }
    }

    public void OnPlayerDeath()
    {
        _isSpawning = false;
    }

    public void SpawnPowerup(Vector3 currentPOS)
    {
        float spawnChance = Random.Range(0f, 1f);

        if (spawnChance <= _powerupSpawnChance)
        {
            int randomPowerup = Random.Range(0, _powerupPrefabs.Length);
            Instantiate(_powerupPrefabs[randomPowerup], currentPOS, Quaternion.identity);
        }
    }
}
