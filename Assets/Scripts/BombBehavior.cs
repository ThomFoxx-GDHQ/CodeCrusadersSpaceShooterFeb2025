using UnityEngine;

public class BombBehavior : MonoBehaviour
{
    [SerializeField] Animator _anim;
    bool _bombTrigger = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Hit Enemy", other.gameObject);
        //Debug.Break();

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                TriggerBomb();
            }
        }

        if (other.CompareTag("Enemy"))
        {            
            Enemy enemy = other.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                enemy.OnEnemyDeath();
                TriggerBomb();
            }
        }
    }

    private void TriggerBomb()
    {
        if (!_bombTrigger)
        {
            _bombTrigger = true;
            _anim.SetBool("BombTriggered", _bombTrigger);
        }
    }

    public void DestroyOnEnd()
    {
        Destroy(this.gameObject);
    }
}
