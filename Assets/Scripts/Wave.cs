using System.Collections;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float _speed = 4.5f;
    [SerializeField] private float _timeInPlay = 1f;
    private Coroutine _lifeCoroutine;
    private WaitForSeconds _timeDelay;

    void OnEnable()
    {
        _timeDelay = new WaitForSeconds(_timeInPlay);
        _lifeCoroutine = StartCoroutine(TimeInPlayRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        transform.Translate(Vector3.right *(_speed * Time.deltaTime));
    }

    IEnumerator TimeInPlayRoutine()
    {
        yield return _timeDelay;
        //return to Pool
        transform.gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
    }
}
