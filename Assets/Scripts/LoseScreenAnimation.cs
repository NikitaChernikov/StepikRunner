using UnityEngine;

public class LoseScreenAnimation : MonoBehaviour
{
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerLost.OnDeath += PlayerLost_OnDeath;
    }

    private void OnDisable()
    {
        PlayerLost.OnDeath -= PlayerLost_OnDeath;
    }

    private void PlayerLost_OnDeath(bool obj)
    {
        _animator.enabled = true;
    }
}
