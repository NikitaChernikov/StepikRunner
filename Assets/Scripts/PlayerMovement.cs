using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Character Parameters")]
    [SerializeField] private float _forwardSpeed = 20f;
    [SerializeField] private float _laneChangeSpeed = 20f;
    [SerializeField] private float _swipeThreshold = 50f;
    [SerializeField] private float _jumpForce = 10f;

    [Header("Map Parameters")]
    [SerializeField] private float _middleLaneX = 2.5f;
    [SerializeField] private float _leftLaneX = -2.5f;
    [SerializeField] private float _rightLaneX = 7.5f;

    public static event Action<Animations> OnChangeAnimation;

    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private float _targetX;
    private bool _isGrounded;

    private Vector2 _startTouchPosition;
    private Vector2 _endTouchPosition;

    private void OnEnable()
    {
        CharacterAcceleration.OnAccelerate += CharacterAcceleration_OnAccelerate;
    }

    private void CharacterAcceleration_OnAccelerate(float amount)
    {
        _forwardSpeed += amount;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _targetX = _middleLaneX;
    }

    private void Update()
    {
        // ������ ��������: ������ ��������� � ������� �������� �� �����
        _moveDirection = transform.forward * _forwardSpeed;

        // ��������� ������� �� ����� ��� ����� ������
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
        // ����������� � ������� ������� �� X
        float newX = Mathf.MoveTowards(transform.position.x, _targetX, _laneChangeSpeed * Time.deltaTime);
        Vector3 targetPosition = new Vector3(newX, transform.position.y, transform.position.z);

        // ������������� ����� ��������, �������� ������������ �������� ��� �������
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
        OnChangeAnimation?.Invoke(Animations.ToLeft);
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
        OnChangeAnimation?.Invoke(Animations.ToRight);
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _isGrounded = false;
            OnChangeAnimation?.Invoke(Animations.StartJump);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _isGrounded = true;
        OnChangeAnimation?.Invoke(Animations.StopJump);
    }
}
