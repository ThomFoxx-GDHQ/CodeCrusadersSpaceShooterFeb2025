using System.Collections;
using UnityEngine;

public class SmartEnemy : MonoBehaviour
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

    private bool _isBehindPlayer = false;
    [SerializeField] private Transform _model;
    private Quaternion _modelDefaultRotation;
    private float _rot_z = 0;
    private bool _dodge = false;
    private int _dodgeIndex = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.FindAnyObjectByType<SpawnManager>();
        _player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        _laserContainer = GameObject.FindGameObjectWithTag("Container")?.transform;

        if (_laserContainer != null)
            _laserPool = _laserContainer.GetComponent<LaserPool>();
        else Debug.LogError("Laser Container is Null", this.gameObject);

        _modelDefaultRotation = _model.rotation;
        _rot_z = _modelDefaultRotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (_isBehindPlayer && Time.time >= _canFireLaser)
            FireLaser();

        if (!_isBehindPlayer && transform.position.x < _player.transform.position.x)
            TrackPlayerToggle(true);
        else if (_isBehindPlayer && transform.position.x >= _player.transform.position.x)
            TrackPlayerToggle(false);
    }

    private void CalculateMovement()
    {

        if (_isBehindPlayer)
        {
            _rot_z = _modelDefaultRotation.eulerAngles.z + 180;
            _model.rotation = Quaternion.Euler(0, 0, _rot_z);
        }
        else if (_model.rotation != _modelDefaultRotation)
        {
            _model.rotation = _modelDefaultRotation;
            TrackPlayerToggle(false);
        }

        if (_dodge)
            transform.Translate(Vector3.up * (_speed * Time.deltaTime * _dodgeIndex));
        else
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

    public void TrackPlayerToggle(bool toggle)
    {
        _isBehindPlayer = toggle;
        _canFireLaser = Time.time + Random.Range(_fireRateRange.x, _fireRateRange.y);
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

    public void DodgeLaser()
    {
        _dodge = true;
        //pick a random number and if greater than .5 dodge up else dodge down
        _dodgeIndex = Random.value >= .5f ? 1 : -1;
        StartCoroutine(DodgeRoutine());
    }

    IEnumerator DodgeRoutine()
    {
        yield return new WaitForSeconds(.5f);
        _dodge = false;
    }
}
