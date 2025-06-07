using UnityEngine;

public class Powerups : MonoBehaviour
{
    public enum PowerupType
    {
        None,
        TripleShot,
        Speed,
        Shield,
        Ammo,
        SpreadShot,
        ReverseControls
    }

    [SerializeField] private PowerupType _powerupID;
    [SerializeField] private float _speed = 5;
    [SerializeField] private AudioClip _powerSound;
    [SerializeField] private int _powerupAmount = 0;

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
                case PowerupType.Ammo:
                    player.AddAmmo(_powerupAmount);
                    break;
                case PowerupType.SpreadShot:
                    player.ActivateSpreadShot();
                    break;
                case PowerupType.ReverseControls:
                    player.ReverseControls();
                    break;
                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(_powerSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}
