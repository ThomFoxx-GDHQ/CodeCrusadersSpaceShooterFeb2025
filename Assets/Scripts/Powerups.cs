using UnityEngine;

public class Powerups : MonoBehaviour
{
    public enum PowerupType
    {
        None,
        TripleShot,
        Speed,
        Shield
    }

    [SerializeField] private PowerupType _powerupID;
    [SerializeField] private float _speed = 5;
    [SerializeField] private AudioClip _powerSound;

    private void Update()
    {
        transform.Translate(Vector3.left * (_speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
             Player player = other.GetComponent<Player>();

            switch (_powerupID)
            {
                case PowerupType.None:
                    break;
                case PowerupType.TripleShot:
                    player.ActivateTripelshot();
                    break;
                case PowerupType.Speed:
                    player.ActivateSpeedBoost();
                    break;
                case PowerupType.Shield:
                    player.ActivateShield();
                    break;
                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(_powerSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}
