using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _rightBounds;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.right * (_speed * Time.deltaTime));
        if (transform.position.x >= _rightBounds)
        {
            transform.gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }            
    }
}
