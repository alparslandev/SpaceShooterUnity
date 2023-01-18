using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject _Shields;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private int _lifePoints = 7;

    [SerializeField]
    private SpawnManager _spawnManager;

    [SerializeField]
    private float _speedMultiplier = 2;

    [SerializeField]
    private float _trippleShotTime = 5.0f;

    [SerializeField]
    private int _score = 0;

    private UIManager _uiManager;

    private bool _isTripleShotActive = false;
    private bool _isImmortal = false;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        calculateMovement();

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && Time.time > _canFire)
        {
            fireLazer();
        }
    }

    public void IncreaseScore(int points = 10)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    public void activateTrippleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(TrippleShotPowerDownRoutine());
    }

    IEnumerator TrippleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(_trippleShotTime);
        _isTripleShotActive = false;
    }

    public void Damage()
    {
        if (_isImmortal) return;

        _lifePoints--;
        _uiManager.UpdateLives(_lifePoints);

        if (this.gameObject.activeInHierarchy && _lifePoints < 1)
        {
            if (_spawnManager != null)
            {
                _spawnManager.OnPlayerDeath();
                _uiManager.UpdatePlayerDeath();
            }
            Destroy(this.gameObject);
        }
    }

    public bool isDead()
    {
        return _lifePoints < 1;
    }

    private void fireLazer()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.0f, 0), Quaternion.identity);
        }
    }

    public void activateSpeedMultiplier()
    {
        _speed *= _speedMultiplier;
        StartCoroutine(SpawnSpeedUpRoutine());
    }

    IEnumerator SpawnSpeedUpRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
    }

    public void activateShield()
    {
        _isImmortal = true;
        _Shields.SetActive(true);
        StartCoroutine(SpawnShieldRoutine());
    }

    IEnumerator SpawnShieldRoutine()
    {
        yield return new WaitForSeconds(5);
        _Shields.SetActive(false);
        _isImmortal = false;
    }

    private void calculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.right *  _speed * horizontalInput * Time.deltaTime);
        //transform.Translate(Vector3.up *  _speed * verticalInput * Time.deltaTime);
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        /*if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }*/

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }
}