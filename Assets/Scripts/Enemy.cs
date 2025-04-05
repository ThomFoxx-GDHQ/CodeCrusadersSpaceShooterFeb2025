using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Enemy Boundaries")]
    [SerializeField] private float _topBound;
    [SerializeField] private float _bottomBound;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;

    private SpawnManager _spawnManager;
    private Player _player;

    [SerializeField] private GameObject _explosionPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * (_speed * Time.deltaTime));
        if (transform.position.x < _leftBound)
        {
            float rng = Random.Range(_bottomBound,_topBound);
            transform.position = new Vector3(_rightBound, rng, 0);
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.Damage();
                _player.AddScore(5);
            }
            OnEnemyDeath();
        }
        if (other.CompareTag("Projectile"))
        {
            //Reset projectile Object to Pool
            other.gameObject.SetActive(false);
            if (other.transform.parent.CompareTag("Container"))
                other.transform.localPosition = Vector3.zero;
            
            _spawnManager.SpawnPowerup(transform.position);
            if (_player !=null) 
                _player.AddScore(10);

            OnEnemyDeath();
        }
    }

    private void OnEnemyDeath()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
