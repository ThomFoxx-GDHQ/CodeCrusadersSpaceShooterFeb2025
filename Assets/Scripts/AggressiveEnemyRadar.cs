using UnityEngine;

public class AggressiveEnemyRadar : MonoBehaviour
{
    [SerializeField] private AggressiveEnemy _parent;

    private void Start()
    {
        if (_parent == null)
            Debug.LogWarning("Radar could not find Parent Script", this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _parent.TrackPlayerToggle(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _parent.TrackPlayerToggle(false);
        }
    }
}
