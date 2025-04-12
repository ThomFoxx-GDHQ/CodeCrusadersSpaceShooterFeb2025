using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    float _horizontalInput;
    float _verticalInput;
    Vector3 _direction = Vector3.zero;
    Vector3 _position = Vector3.zero;

    [Header("Player Boundary")]
    [SerializeField] private float _topBound;
    [SerializeField] private float _bottomBound;
    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;

    [SerializeField] private LaserPool _laserPool;
    [SerializeField] private float _laserFireRate = 1f;
    private float _whenCanLaserFire = -1;
    [SerializeField] private Vector3 _laserOffset = Vector3.zero;
    [SerializeField] private float _waveFireRate = 1f;
    private float _whenCanWaveFire = -1;
    [SerializeField] private bool _isTripleShotActive = false;
    [SerializeField] private AudioClip _laserAudio;
    [SerializeField] private AudioClip _waveAudio;

    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField] private float _speedBoostMultipler = 2.5f;
    private float _speedMultiplier = 1;

    [SerializeField] private GameObject _shieldVisual;
    private bool _isShieldActive;

    private int _score;
    private UIManager _uiManager;

    [SerializeField] private GameObject[] _damageEffects;

    [SerializeField] private bool _isDead = false;
    [SerializeField] private AudioSource _audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spawnManager = GameObject.Find("Managers")?.GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Managers")?.GetComponent<UIManager>();

        _shieldVisual.SetActive(false);
        
        foreach (GameObject go in _damageEffects)
        {
            go.SetActive(false);
        }

        _uiManager.UpdateScore(_score);
        _uiManager.UpdateLives(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDead == true) return;

        CalculateMovement();
        CalculateBoundary();

        if (Input.GetKey(KeyCode.Space) && _whenCanLaserFire < Time.time)
        {
           FireLaser();
        }
        if (Input.GetKey(KeyCode.RightShift) && _whenCanWaveFire < Time.time)
        {
            FireWave();
        }
    }

    private void CalculateMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        _direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(_direction * (_speed * _speedMultiplier * Time.deltaTime));
    }

    private void CalculateBoundary()
    {
        _position = transform.position;

        if (transform.position.y < _bottomBound)
        {
            _position.y = _bottomBound;
        }
        if (transform.position.y > _topBound)
        {
            _position.y = _topBound;
        }
        if (transform.position.x < _leftBound)
        {
            _position.x = _leftBound;
        }
        if (transform.position.x > _rightBound)
        {
            _position.x = _rightBound;
        }

        transform.position = _position;
    }

    private void FireLaser()
    {
        if (_isTripleShotActive)
        {
            _laserPool.GetTripleShot(transform.position);
        }
        else
        {
            _laserPool.GetLaser(transform.position + _laserOffset);
            //go.transform.parent = _laserContainer;
        }

        _audioSource.clip = _laserAudio;
        _audioSource.Play();

        _whenCanLaserFire = Time.time + _laserFireRate;
    }

    private void FireWave()
    {
        _laserPool.GetWave(transform.position);

        _audioSource.clip = _waveAudio;
        _audioSource.Play();

        _whenCanWaveFire = Time.time + _waveFireRate;
    }

    public void Damage()
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shieldVisual.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        
        if (_lives <=0)
        {
            _spawnManager.OnPlayerDeath();
           _uiManager.GameOver();
            GameObject.Find("Managers").GetComponent<GameManager>()?.GameOver();
            //Destroy(this.gameObject);
            transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<Collider>().enabled = false;
            _isDead = true;
        }
        else if (_lives == 2)
        {
            _damageEffects[0].SetActive(true);
        }
        else if (_lives == 1)
        {
            _damageEffects[1].SetActive(true);
        }
    }

    public void ActivateTripelshot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotActive = false;
    }

    public void ActivateSpeedBoost()
    {
        _speedMultiplier = _speedBoostMultipler;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _speedMultiplier = 1;
    }

    public void ActivateShield()
    {
        _isShieldActive = true;
        _shieldVisual.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
