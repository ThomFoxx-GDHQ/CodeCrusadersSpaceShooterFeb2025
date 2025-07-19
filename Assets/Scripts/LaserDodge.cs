using UnityEngine;

public class LaserDodge : MonoBehaviour
{
    [SerializeField] SmartEnemy _parent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            /*Remove Comments to not dadge own laser
            if (other.TryGetComponent<Laser>(out Laser laser))
                if (laser.IsEnemyLaser)
                    return;*/

            _parent.DodgeLaser();
        }
    }
}
