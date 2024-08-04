using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Parameters")]
    [SerializeField] private float _forwardSpeed = 20f;
    [SerializeField] private float _laneChangeSpeed = 20f;
    [SerializeField] private float _swipeThreshold = 50f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private PlayerAnimations _playerAnimations;

    [Header("Map Parameters")]
    [SerializeField] private float _middleLaneX = 2.5f;
    [SerializeField] private float _leftLaneX = -2.5f;
    [SerializeField] private float _rightLaneX = 7.5f;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private float _targetX;
    private bool _isGrounded;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetX = _middleLaneX;
    }

    private void Update()
    {
        // Вектор движения: вперед постоянно и боковое движение по вводу
        _moveDirection = transform.forward * _forwardSpeed;

        // Обработка нажатий на экран для смены полосы
        if (Input.GetMouseButtonDown(0))
        {
            _startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _endTouchPosition = Input.mousePosition;
            Vector2 swipeDelta = _endTouchPosition - _startTouchPosition;

            if (Mathf.Abs(swipeDelta.y) > _swipeThreshold && swipeDelta.y > 0)
            {
                Jump();
            }

            else if (Mathf.Abs(swipeDelta.x) > _swipeThreshold)
            {
                if (swipeDelta.x < 0)
                {
                    MoveLeft();
                }
                else if (swipeDelta.x > 0)
                {
                    MoveRight();
                }
            }
        }
    }

    private void FixedUpdate()
    {
        // Перемещение к целевой позиции по X
        float newX = Mathf.MoveTowards(transform.position.x, _targetX, _laneChangeSpeed * Time.deltaTime);
        Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);

        // Устанавливаем новую скорость, сохраняя вертикальную скорость для прыжков
        _rigidbody.MovePosition(targetPosition + _moveDirection * Time.fixedDeltaTime);
    }

    private void MoveLeft()
    {
        if (_targetX == _middleLaneX)
        {
            _targetX = _leftLaneX;
        }
        else if (_targetX == _rightLaneX)
        {
            _targetX = _middleLaneX;
        }
        _playerAnimations.ChangeAnim(_playerAnimations.LeftParameter);
    }

    private void MoveRight()
    {
        if (_targetX == _middleLaneX)
        {
            _targetX = _rightLaneX;
        }
        else if (_targetX == _leftLaneX)
        {
            _targetX = _middleLaneX;
        }
        _playerAnimations.ChangeAnim(_playerAnimations.RightParameter);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
            _playerAnimations.ChangeAnim(_playerAnimations.JumpStartParameter);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        _playerAnimations.ChangeAnim(_playerAnimations.JumpEndParameter);
        _playerAnimations.ResetLaneChangeTriggers();
    }
}
