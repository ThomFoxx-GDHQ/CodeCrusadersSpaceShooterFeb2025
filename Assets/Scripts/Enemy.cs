using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed;

    [Header("Enemy Boundaries")]
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
            other.GetComponent<Player>()?.Damage();
            Destroy(this.gameObject);
        }
        if (other.CompareTag("Projectile"))
        {
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            other.transform.localPosition = Vector3.zero;
            Destroy(this.gameObject);
        }
    }
}
