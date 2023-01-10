using Assets.Scripts.Utils;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

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
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.activateTrippleShot();
            }
            Destroy(gameObject);
        }
    }
}
