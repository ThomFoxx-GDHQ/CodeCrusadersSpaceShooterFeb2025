using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    private int _childCount = 0;
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
        {
            
            transform.parent.Translate(Vector3.right * (_speed * Time.deltaTime / ActiveChildCount()));
        }
        else
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));

        if (transform.position.x >= _rightBounds)
        {
            ResetObject();
        }
    }

    private int ActiveChildCount()
    {        
        Laser[] children = transform.parent.GetComponentsInChildren<Laser>();
        _childCount = 0;
        foreach (var child in children)
        {
            if (child.gameObject.activeInHierarchy)
                _childCount++;
        }
        return _childCount;
    }

    public void ResetObject()
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
            Laser[] children = transform.parent.GetComponentsInChildren<Laser>();
            foreach (var child in children)
            {
                child.gameObject.SetActive(true);
            }
        }
    }
}
