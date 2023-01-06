using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    void Update()
    {
        // move down at 4 meters per second
        // if bottom of screen respawn at top with a new random x position

        if (transform.position.y <= -3.8f)
        {
            transform.position = getRandomVector3();
            GameObject playerGameObj = GameObject.FindWithTag("Player");
            if (playerGameObj != null)
            {
                Player playerObj = playerGameObj.GetComponent<Player>();
                if (!playerObj.isDead())
                    playerObj.Damage();
            } 
            else
            {
                Destroy(gameObject);
            }
        }

        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            var player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            Destroy(gameObject);
        }
    }

    private Vector3 getRandomVector3()
    {
        var rand = Random.Range(-9f, 9f);
        return new Vector3(rand, 6, 0);
    }
}
