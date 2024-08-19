using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private float _coinWorth = 20;

    public static event Action<float> OnScoreChanged;
    public static event Action OnNewBestScore;
    public static string ScoreKey = "Score";

    private bool _isPlayerDead;
    private float _score = 0;

    private void OnEnable()
    {
        PlayerLost.OnDeath += PlayerLost_OnDeath;
        CoinCollector.OnCollectCoin += CoinCollector_OnCollectCoin;
    }

    private void OnDisable()
    {
        PlayerLost.OnDeath -= PlayerLost_OnDeath;
        CoinCollector.OnCollectCoin -= CoinCollector_OnCollectCoin;
    }

    private void CoinCollector_OnCollectCoin(Vector3 pos)
    {
        _score += _coinWorth;
    }

    private void PlayerLost_OnDeath(bool isDead)
    {
        _isPlayerDead = isDead;
        if (PlayerPrefs.HasKey(ScoreKey))
        {
            if (_score > PlayerPrefs.GetFloat(ScoreKey))
            {
                PlayerPrefs.SetFloat(ScoreKey, _score);
                OnNewBestScore?.Invoke();
            }
        }
        else
        {
            PlayerPrefs.SetFloat(ScoreKey, _score);
            OnNewBestScore?.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlayerDead)
        {
            _score += Time.deltaTime;
            OnScoreChanged?.Invoke(_score);
        }
    }
}
