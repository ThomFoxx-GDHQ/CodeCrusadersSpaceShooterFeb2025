using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] Vector2 _limits;
    Vector3 _originalPosition;
    float _originalZPosition;
    [SerializeField] float _shakeTime;

    private void Start()
    {
        _originalPosition = transform.position;
        _originalZPosition = _originalPosition.z;
    }

    [ContextMenu("Shake Camera")]
    public void ShakeCamera()
    {
        StartCoroutine(CameraShakeRoutine());
    }

    IEnumerator CameraShakeRoutine()
    {
        float timer = 0;
        while (timer <= _shakeTime)
        {
            float randomX = Random.value * _limits.x;
            float randomY = Random.value * _limits.y;
            transform.position = new Vector3(randomX, randomY, _originalZPosition);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = _originalPosition;

    }
}
