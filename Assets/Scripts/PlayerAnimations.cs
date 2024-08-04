using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public string LeftParameter = "ToLeft";
    public string RightParameter = "ToRight";
    public string JumpStartParameter = "StartJump";
    public string JumpEndParameter = "StopJump";

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void ChangeAnim(string parameter)
    {
        _animator.SetTrigger(parameter);
    }

    public void ResetLaneChangeTriggers()
    {
        // —брасываем триггеры смены полосы
        _animator.ResetTrigger(LeftParameter);
        _animator.ResetTrigger(RightParameter);
    }
}
