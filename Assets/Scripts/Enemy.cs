using UnityEngine;
using Assets.Scripts.Utils;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private Animator _animator;

    private Player _player;

    private AudioSource _explosionSound;

    private void Start()
    {
        _explosionSound = gameObject.GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
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

            _speed = 0;
            Destroy(gameObject, 2.8f);
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(EnableBox(2.8f));
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(other.gameObject);
            _explosionSound.Play();
        }
        else if (other.tag == "Player")
        {
            if (_player != null)
            {
                _player.Damage();
            }
            
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _explosionSound.Play();
            Destroy(gameObject, 2.8f);
        }
    }

    IEnumerator EnableBox(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
