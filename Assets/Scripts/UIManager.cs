using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _livesDisplay;
    [SerializeField] private Sprite[] _sprites;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private string _gameOverMessage;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private Image _thrusterBar;
    [SerializeField] private Image _shieldDisplay;
    [SerializeField] private Sprite[] _shieldSprites;
    [SerializeField] private TMP_Text _ammoDisplay;
    [SerializeField] private TMP_Text _waveDisplay;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
    }

    public void UpdateLives(int lives)
    {
        if (lives >= 0 && lives < _sprites.Length)
            _livesDisplay.sprite = _sprites[lives];
        else _livesDisplay.sprite = _sprites[0];
    }

    public void UpdateThruster(float thrusterValue, float thrusterMax)
    {
        float percent = thrusterValue / thrusterMax;
        _thrusterBar.fillAmount = percent;
    }

    public void UpdateShieldDisplay(int shieldValue)
    {

        if (shieldValue > 0 && shieldValue < _shieldSprites.Length)
        {
            _shieldDisplay.enabled = true;
            _shieldDisplay.sprite = _shieldSprites[shieldValue];
        }
        if (shieldValue == 0)
            _shieldDisplay.enabled = false;
    }

    public void UpdateAmmoDisplay(int ammoValue)
    {
        _ammoDisplay.text = $"Ammo: {ammoValue}";
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        while (true)
        {
            yield return null;

            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            _gameOverText.text = "";
            for (int i = 0; i < _gameOverMessage.Length; i++)
            {
                _gameOverText.text += _gameOverMessage[i];
                yield return new WaitForSeconds(.1f);
            }

            for (int j = 0; j <= 5; j++)
            {
                yield return new WaitForSeconds(.5f);
                _gameOverText.gameObject.SetActive(false);
                yield return new WaitForSeconds(.5f);
                _gameOverText.gameObject.SetActive(true);
            }
        }
    }

    public void UpdateWaveDisplay(int waveNumber)
    {
        _waveDisplay.text = $"Wave: #{waveNumber}";
    }
}
