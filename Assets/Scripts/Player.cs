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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        CalculateBoundary();
        
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
}
