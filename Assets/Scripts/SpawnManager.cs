using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int[] _enemyWeights;
    private int _enemyTotal;
    private float _randomNumber;
    private GameObject _randomEnemy;
    private Vector3 _spawnPOS = Vector3.zero;
    private WaitForSeconds _spawnDelayTimer;
    [SerializeField] private float _spawnTime;
    private bool _isSpawning = true;
    [SerializeField] float _powerupSpawnChance = .5f;
    [SerializeField] private GameObject[] _powerupPrefabs;
    [SerializeField] private int[] _powerupWeights;
    private int _powerupTotal;
    [SerializeField] private Transform _enemyContainer;
    int _waveCounter;
    [SerializeField] int _waveCountMultiplier = 5;
    int _enemiesSpawned = 0;
    int _enemiesInWave = 0;
    [SerializeField] UIManager _uiManager;

    private void Start()
    {
        SpawnWeightsInitialization();

        _spawnDelayTimer = new WaitForSeconds(_spawnTime);

        _waveCounter = 1;
        StartCoroutine(WaveSystemRoutine());
    }

    private void SpawnWeightsInitialization()
    {
        for (int i = 0; i < _enemyWeights.Length; i++)
        {
            _enemyTotal += _enemyWeights[i];
        }
        for (int j = 0; j < _powerupWeights.Length; j++)
        {
            _powerupTotal += _powerupWeights[j];
        }
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
            _randomEnemy = GetRandomEnemny();
            _randomNumber = Random.Range(-5, 5);
            _spawnPOS.y = _randomNumber;
            _spawnPOS.x = 15;
            Instantiate(_randomEnemy, _spawnPOS, Quaternion.identity, _enemyContainer);
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
        Debug.Log($"Spawn Chance Roll: {spawnChance}");
        if (spawnChance <= _powerupSpawnChance)
        {            
            GameObject randomPowerup = null;

            int randomPick = Random.Range(0, _powerupTotal);
            for (int i =0; i < _powerupWeights.Length; i++)
            {
                if (randomPick < _powerupWeights[i])
                {
                    randomPowerup = _powerupPrefabs[i];
                    break;
                }
                else
                    randomPick -= _powerupWeights[i];
            }
            Debug.Log($"Spawning {randomPowerup.name}");
            if (randomPowerup != null) 
                Instantiate(randomPowerup, currentPOS, Quaternion.identity);
        }
    }

    private GameObject GetRandomEnemny()
    {
        GameObject enemy = _enemyPrefabs[0];

        int randomPick = Random.Range(0, _enemyTotal);

        for (int i  = 0; i < _enemyWeights.Length; i++)
        {
            if (randomPick < _enemyWeights[i])            
                return _enemyPrefabs[i];
            else
                randomPick -= _enemyWeights[i];
        }

        return enemy;
    }
}
