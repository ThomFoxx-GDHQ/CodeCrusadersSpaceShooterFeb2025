using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _rightBounds;
    [SerializeField, Tooltip("This should be marked if the laser prefab is inside a Parent prefab Object.")]
    private bool _moveParent;

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        
    }

    void CalculateMovement()
    {
        if (_moveParent == true)
            transform.parent.Translate((Vector3.right * (_speed * Time.deltaTime)) / transform.parent.childCount);
        else
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));

        if (transform.position.x >= _rightBounds)
        {
            if (transform.parent.CompareTag("Container"))
            {
                transform.gameObject.SetActive(false);
                transform.localPosition = Vector3.zero;
            }
            else
            {
                transform.parent.gameObject.SetActive(false);
                transform.parent.localPosition = Vector3.zero;
            }
        }
    }
}
