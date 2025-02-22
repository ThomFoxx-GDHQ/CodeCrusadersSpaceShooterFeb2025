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
    [SerializeField] private float _waveFireRate = 1f;
    private float _whenCanWaveFire = -1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CalculateBoundary();

        if (Input.GetKey(KeyCode.Space) && _whenCanLaserFire < Time.time)
        {
           FireLaser();
        }
        if (Input.GetKey(KeyCode.RightAlt) && _whenCanWaveFire < Time.time)
        {
            FireWave();
        }
    }

    private void CalculateMovement()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        _direction = new Vector3(_horizontalInput, _verticalInput, 0);

        transform.Translate(_direction * (_speed * Time.deltaTime));
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
        _laserPool.GetLaser(transform.position);
        //go.transform.parent = _laserContainer;

        _whenCanLaserFire = Time.time + _laserFireRate;
    }

    private void FireWave()
    {
        _laserPool.GetWave(transform.position);

        _whenCanWaveFire = Time.time + _waveFireRate;
    }
}
