using UnityEngine;

public class BomberEnemy : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] GameObject _bombPrefab;
    [SerializeField] Transform[] _dropPoints;
    float _timer = 1;
    int _randDropPoint = 0;
    Transform _enemyContainer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyContainer = GameObject.Find("EnemyContainer").transform;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        DropRoutine();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.left * (_speed * Time.deltaTime));
        if (transform.position.x < -15)
            Destroy(this.gameObject);
    }

    private void DropRoutine()
    {
        if (_timer < Time.time)
        {
            _randDropPoint = Random.Range(0, _dropPoints.Length);
            Instantiate(_bombPrefab, _dropPoints[_randDropPoint].position, Quaternion.identity, _enemyContainer);
            _timer = Time.time + Random.Range(2, 5);
        }
    }
}
