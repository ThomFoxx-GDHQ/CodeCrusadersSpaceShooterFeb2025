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
            _spawnPOS.x = 12;
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
        //Let's set this up to have a random chance to spawn when called.

        Instantiate(_powerupPrefabs[0], currentPOS, Quaternion.identity);
    }
}
