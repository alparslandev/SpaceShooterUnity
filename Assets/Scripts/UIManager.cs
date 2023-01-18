using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private TextMeshProUGUI _gameOverText;

    [SerializeField]
    private TextMeshProUGUI _restartLevelText;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Image _LivesImage;

    void Start()
    {
        UpdateScore(0);
        _gameOverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _restartLevelText.gameObject.activeInHierarchy)
        {
            StopCoroutine(GameOverFlickerRoutine());
            Start();
            SceneManager.LoadScene(0);
        }
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _liveSprites[currentLives];
    }

    public void UpdatePlayerDeath()
    {
        _restartLevelText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
