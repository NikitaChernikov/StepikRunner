using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private float _score = 0;
    [SerializeField] private float _coinWorth = 20;

    private bool _isPlayerDead;

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

    private void CoinCollector_OnCollectCoin()
    {
        _score += _coinWorth;
    }

    private void PlayerLost_OnDeath(bool isDead)
    {
        _isPlayerDead = isDead;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isPlayerDead)
        {
            _score += Time.deltaTime;
        }
    }
}
