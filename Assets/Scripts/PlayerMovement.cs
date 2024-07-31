using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementRigidbody : MonoBehaviour
{
    [SerializeField] private float _forwardSpeed = 10f;
    
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Вектор движения: вперед постоянно и боковое движение по вводу
        _moveDirection = transform.forward * _forwardSpeed;
    }

    private void FixedUpdate()
    {
        // Устанавливаем новую скорость, сохраняя вертикальную скорость для прыжков
        _rigidbody.MovePosition(_rigidbody.position + _moveDirection * Time.fixedDeltaTime);
    }
}
