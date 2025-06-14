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
    [SerializeField] private GameObject _laserPrefab;
    private float _canFireLaser = 0;
    private Transform _laserContainer;
    [SerializeField] private Transform _laserFirePosition;
    private LaserPool _laserPool;
    [Tooltip("X = Mininmum Fire Time, Y = Maximum Fire Time")]
    [SerializeField] private Vector2 _fireRateRange;
    Animator _animator;
    [SerializeField, Range(0,1)] float _waveEnemyChance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _laserContainer = GameObject.FindGameObjectWithTag("Container")?.transform;
        _animator = transform.GetChild(0).GetComponent<Animator>();

        if (_laserContainer != null)
            _laserPool = _laserContainer.GetComponent<LaserPool>();
        else Debug.LogError("Laser Container is Null", this.gameObject);

        float rng = Random.value;
        if (rng <= _waveEnemyChance )
        {
            _animator.SetBool("IsWaveEnemy", true);
            _animator.SetFloat("Offset", rng);
            float rndDirection = Random.value;
            if (rndDirection >= .5f)
                _animator.SetInteger("Direction", -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Time.time >= _canFireLaser)
            FireLaser();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.left * (_speed * Time.deltaTime));
        if (transform.position.x < _leftBound)
        {
            float rng = Random.Range(_bottomBound, _topBound);
            transform.position = new Vector3(_rightBound, rng, 0);
        }
    }

    private void FireLaser()
    {
        _laserPool.GetLaser(_laserFirePosition.position, true);        
        _canFireLaser = Time.time + Random.Range(_fireRateRange.x, _fireRateRange.y);
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
            if (other.TryGetComponent<Laser>(out Laser laser))
                if (laser.IsEnemyLaser) return;

            //Reset projectile Object to Pool
            other.gameObject.SetActive(false);
            //if (other.transform.parent.CompareTag("Container"))
            //    other.transform.localPosition = Vector3.zero;
            
            _spawnManager.SpawnPowerup(transform.position);
            if (_player !=null) 
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
