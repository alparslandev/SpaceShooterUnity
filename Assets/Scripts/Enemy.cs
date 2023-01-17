using UnityEngine;
using Assets.Scripts.Utils;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (transform.position.y <= -3.8f)
        {
            transform.position = Utils.getRandomVector3();
            if (_player != null)
            {
                Player playerObj = _player.GetComponent<Player>();
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
            if (_player != null)
            {
                _player.IncreaseScore(10);
            }
            
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            Destroy(gameObject);
        }
    }
}
