using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    private float _randomNumber;
    private int _randomEnemy;
    private Vector3 _spawnPOS = Vector3.zero;
    private WaitForSeconds _spawnDelayTimer;
    [SerializeField] private float _spawnTime;
    private bool _isSpawning = true;
    [SerializeField] float _powerupSpawnChance = .5f;
    [SerializeField] private GameObject[] _powerupPrefabs;
    [SerializeField] private Transform _enemyContainer;
    int _waveCounter;
    [SerializeField] int _waveCountMultiplier = 5;
    int _enemiesSpawned = 0;
    int _enemiesInWave = 0;
    [SerializeField] UIManager _uiManager;

    private void Start()
    {
        _spawnDelayTimer = new WaitForSeconds(_spawnTime);

        _waveCounter = 1;
        StartCoroutine(WaveSystemRoutine());
    }

    IEnumerator WaveSystemRoutine()
    {
        while (_isSpawning)
        {
            _uiManager.UpdateWaveDisplay(_waveCounter);
            _enemiesInWave = _waveCounter * _waveCountMultiplier;

            yield return StartCoroutine(EnemySpawnRoutine());

            while (_enemyContainer.childCount > 0)
            {
                yield return new WaitForSeconds(5f);
            }
            _waveCounter++;
        }
    }

    IEnumerator EnemySpawnRoutine()
    {
        _enemiesSpawned = 0;

        while (_isSpawning == true && _enemiesSpawned < _enemiesInWave)
        {
            _randomEnemy = Random.Range(0, _enemyPrefabs.Length);
            _randomNumber = Random.Range(-5, 5);
            _spawnPOS.y = _randomNumber;
            _spawnPOS.x = 15;
            Instantiate(_enemyPrefabs[_randomEnemy], _spawnPOS, Quaternion.identity, _enemyContainer);
            _enemiesSpawned++;
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
