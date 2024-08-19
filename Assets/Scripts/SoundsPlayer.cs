using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip _coinSound, _jumpStartSound, _jumpEndSound, _gameOverSound;
    [SerializeField] private AudioSource _musicSource, _soundsSource;

    private void OnEnable()
    {
        PlayerMovement.OnChangeAnimation += PlayerMovement_OnChangeAnimation;
        CoinCollector.OnCollectCoin += CoinCollector_OnCollectCoin;
        PlayerLost.OnDeath += PlayerLost_OnDeath;
    }

    private void OnDisable()
    {
        PlayerMovement.OnChangeAnimation -= PlayerMovement_OnChangeAnimation;
        CoinCollector.OnCollectCoin -= CoinCollector_OnCollectCoin;
        PlayerLost.OnDeath -= PlayerLost_OnDeath;
    }

    private void PlayerMovement_OnChangeAnimation(Animations anim)
    {
        switch (anim)
        {
            case Animations.StartJump:
                PlaySound(_jumpStartSound);
                break;
            case Animations.StopJump:
                PlaySound(_jumpEndSound);
                break;
            default:
                break;
        }
    }

    private void CoinCollector_OnCollectCoin(Vector3 obj)
    {
        PlaySound(_coinSound);
    }

    private void PlayerLost_OnDeath(bool obj)
    {
        _musicSource.Stop();
        PlaySound(_gameOverSound);
    }

    private void PlaySound(AudioClip clip)
    {
        _soundsSource.clip = clip;
        _soundsSource.Play();
    }
}
