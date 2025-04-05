using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float _delayTime;

    void Start()
    {
        Destroy(this.gameObject, _delayTime);
    }

}
