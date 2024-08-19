using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private GameObject _loseScreen;

    private void OnEnable()
    {
        ScoreCounter.OnScoreChanged += ScoreCounter_OnScoreChanged;
        PlayerLost.OnDeath += PlayerLost_OnDeath;
    }

    private void OnDisable()
    {
        ScoreCounter.OnScoreChanged -= ScoreCounter_OnScoreChanged;
        PlayerLost.OnDeath -= PlayerLost_OnDeath;
    }

    private void ScoreCounter_OnScoreChanged(float score)
    {
        _scoreText.text = score.ToString("F1");
    }

    private void PlayerLost_OnDeath(bool obj)
    {
        _loseScreen.SetActive(true);
    }
}
