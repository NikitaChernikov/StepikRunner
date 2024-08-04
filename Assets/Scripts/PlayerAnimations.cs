using UnityEngine;

public enum Animations
{
    ToLeft,
    ToRight,
    StartJump,
    StopJump
}

public class PlayerAnimations : MonoBehaviour
{
    private string _leftParameter = "ToLeft";
    private string _rightParameter = "ToRight";
    private string _jumpStartParameter = "StartJump";
    private string _jumpEndParameter = "StopJump";

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        PlayerMovement.OnChangeAnimation += PlayerMovement_OnChangeAnimation;
    }

    private void OnDisable()
    {
        PlayerMovement.OnChangeAnimation -= PlayerMovement_OnChangeAnimation;
    }

    private void PlayerMovement_OnChangeAnimation(Animations anim)
    {
        switch (anim)
        {
            case Animations.ToLeft:
                ChangeAnim(_leftParameter);
                break;
            case Animations.ToRight:
                ChangeAnim(_rightParameter);
                break;
            case Animations.StartJump:
                ChangeAnim(_jumpStartParameter);
                break;
            case Animations.StopJump:
                ChangeAnim(_jumpEndParameter);
                ResetLaneChangeTriggers();
                break;
        }
    }

    public void ChangeAnim(string parameter)
    {
        _animator.SetTrigger(parameter);
    }

    private void ResetLaneChangeTriggers()
    {
        // —брасываем триггеры смены полосы
        _animator.ResetTrigger(_leftParameter);
        _animator.ResetTrigger(_rightParameter);
    }
}