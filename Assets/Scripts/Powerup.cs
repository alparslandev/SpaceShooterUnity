using Assets.Scripts.Utils;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    // ID for Powerups
    // 0 = Triple Shot
    // 1 = Speed
    // 2 = Shield
    [SerializeField]
    private int powerupID;

    [SerializeField]
    private AudioClip _clip;

    void Update()
    {
        if (transform.position.y <= -3.5f)
        {
            transform.position = Utils.getRandomVector3();
            Destroy(gameObject);
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(_clip, transform.position, 1.0f);
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                        player.activateTrippleShot();
                        break;
                    case 1:
                        player.activateSpeedMultiplier();
                        break;
                    default:
                        player.activateShield();
                        break;
                }
                Destroy(gameObject);
            }
        }
    }
}
