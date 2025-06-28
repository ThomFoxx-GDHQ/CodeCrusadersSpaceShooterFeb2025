using UnityEngine;

public class AggressiveEnemy : MonoBehaviour
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

    private bool _isTrackingPlayer = false;
    [SerializeField] private Transform _model;
    private Quaternion _modelDefaultRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _modelDefaultRotation = _model.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        if (!_isTrackingPlayer)
        {
            if (_model.rotation != _modelDefaultRotation) 
                _model.rotation = _modelDefaultRotation;

            transform.Translate(Vector3.left * (_speed * Time.deltaTime));
            if (transform.position.x < _leftBound)
            {
                float rng = Random.Range(_bottomBound, _topBound);
                transform.position = new Vector3(_rightBound, rng, 0);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.transform.position, _speed * Time.deltaTime);
            LookatTarget();
        }
    }

    private void LookatTarget()
    {
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();

        float rot_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _model.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);

    }

    public void TrackPlayerToggle(bool toggle)
    {
        _isTrackingPlayer = toggle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _player?.Damage();

            if (_player != null)
            {
                _player.AddScore(5);
            }
            OnEnemyDeath();

        }
        else if (other.CompareTag("Projectile"))
        {
            if (other.TryGetComponent<Laser>(out Laser laser))
                if (laser.IsEnemyLaser) return;

            other.gameObject.SetActive(false);

            _spawnManager.SpawnPowerup(transform.position);
            if (_player != null)
                _player.AddScore(10);

            OnEnemyDeath();

        }
    }

    public void OnEnemyDeath()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

        Destroy(this.gameObject);
    }
}
