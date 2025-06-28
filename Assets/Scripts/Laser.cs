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
    private bool _isEnemyLaser = false;
    private bool _isFireLeft = false;
    private Vector3 _direction = Vector3.zero;
    private Vector3 _localPostion;

    private void Start()
    {
        _localPostion = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if (_isEnemyLaser && _isFireLeft)
            _direction = Vector3.left;
        else 
            _direction = Vector3.right;

        if (_moveParent == true)
        {

            transform.parent.Translate(_direction * (_speed * Time.deltaTime / ActiveChildCount()));
        }
        else
            transform.Translate(_direction * (_speed * Time.deltaTime));

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
            Laser[] children = transform.parent.GetComponentsInChildren<Laser>();
            foreach (var child in children)
            {
                child.gameObject.SetActive(true);
                //child.transform.localPosition = child.GetSavedLocal;
                child.transform.localPosition = Vector3.zero;
            }
            transform.parent.gameObject.SetActive(false);
            transform.parent.localPosition = Vector3.zero;
        }
        SetEnemyLaser(false);
    }

    public void SetEnemyLaser(bool isEnemyLaser)
    {
        _isEnemyLaser = isEnemyLaser;
    }

    public void SetFireDirectionLeft(bool isFireDirectionLeft)
    {
        _isFireLeft = isFireDirectionLeft;
    }

    public bool IsEnemyLaser
    {
        get { return _isEnemyLaser; }
    }
      

    public Vector3 GetSavedLocal => _localPostion;
}
