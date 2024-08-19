using UnityEngine;
using TMPro;

public class BestScoreUI : MonoBehaviour
{
    [Header("For Game Scene")]
    [SerializeField] private GameObject _bestScoreImage;

    [Header("For Menu Scene")]
    [SerializeField] private TMP_Text _bestScoreText;

    private void OnEnable()
    {
        if (_bestScoreImage != null)
        {
            _bestScoreImage.SetActive(false);
        }
        ScoreCounter.OnNewBestScore += ScoreCounter_OnNewBestScore;
    }

    private void Start()
    {
        if (_bestScoreText != null)
        {
            if (PlayerPrefs.HasKey(ScoreCounter.ScoreKey))
            {
                _bestScoreText.text = PlayerPrefs.GetFloat(ScoreCounter.ScoreKey).ToString("F1");
            }
            else
            {
                _bestScoreText.text = "0";
            }
        }
    }

    private void OnDisable()
    {
        ScoreCounter.OnNewBestScore -= ScoreCounter_OnNewBestScore;
    }

    private void ScoreCounter_OnNewBestScore()
    {
        _bestScoreImage.SetActive(true);
    }
}
