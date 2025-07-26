using Unity.Mathematics;
using UnityEngine;

public class HomingProjectile : MonoBehaviour
{
    [SerializeField] float _speed = 5f;
    private GameObject _target;

    private GameObject[] _potentialTargets;
    private float _closestDistance = Mathf.Infinity;
    private GameObject _closestTarget;

    [SerializeField] private Transform _model;

    private void Start()
    {
        FindClosest();
    }

    private void FindClosest()
    {
        _potentialTargets = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < _potentialTargets.Length; i++)
        {
            if (Vector3.Distance(transform.position, _potentialTargets[i].transform.position) < _closestDistance)
            {
                _closestDistance = Vector3.Distance(transform.position, _potentialTargets[i].transform.position);
                _closestTarget = _potentialTargets[i];
            }
        }

        if (_closestTarget != null)
            _target = _closestTarget;
    }

    private void Update()
    {
        if (_target == null)
        {
            transform.Translate(Vector3.right * (_speed * Time.deltaTime));
            _model.rotation = LookAt(transform.position + Vector3.right);
            FindClosest();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _speed * Time.deltaTime);
            _model.rotation = LookAt(_target.transform.position);
        }
    }

    private quaternion LookAt(Vector3 target)
    {
        Vector3 diff = target - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0f, 0f, rot_z -90);
    }

}
