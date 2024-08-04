using System;
using UnityEngine;

public class PlayerLost : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public static event Action<bool> OnDeath;

    private Rigidbody[] _ragdollRigidbodies;
    private Collider[] _ragdollColliders;
    private PlayerMovement _playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        //_animator = GetComponent<Animator>();
        _playerMovement = GetComponent<PlayerMovement>();

        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _ragdollColliders = GetComponentsInChildren<Collider>();

        DisableRagdoll();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            Debug.Log("Hit");
            _playerMovement.enabled = false;
            EnableRagdoll();
            OnDeath?.Invoke(true);
        }
    }

    // Включаем рэгдолл, отключая анимацию
    public void EnableRagdoll()
    {
        _animator.enabled = false;
        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = false;
        }
        foreach (Collider collider in _ragdollColliders)
        {
            collider.isTrigger = false;
        }
        _ragdollColliders[0].isTrigger = true;
    }

    // Отключаем рэгдолл, включая анимацию
    public void DisableRagdoll()
    {
        foreach (Rigidbody rb in _ragdollRigidbodies)
        {
            rb.isKinematic = true;
        }
        foreach (Collider collider in _ragdollColliders)
        {
            collider.isTrigger = true;
        }
        _ragdollRigidbodies[0].isKinematic = false;
        _ragdollColliders[0].isTrigger = false;
    }
}
