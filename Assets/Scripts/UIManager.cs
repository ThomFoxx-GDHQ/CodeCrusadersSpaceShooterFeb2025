using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _livesDisplay;
    [SerializeField] private Sprite[] _sprites;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"Score: {score}";        
    }

    public void UpdateLives(int lives)
    {
        if (lives >= 0 && lives < _sprites.Length)
            _livesDisplay.sprite = _sprites[lives];
        else _livesDisplay.sprite = _sprites[0];
    }
}
