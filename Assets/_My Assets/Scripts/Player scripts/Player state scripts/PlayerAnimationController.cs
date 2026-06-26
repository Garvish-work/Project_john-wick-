using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private InputConfig inputConfig;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private Animator playerAnimator;

    public void UpdateAnimation()
    {
        playerAnimator.SetFloat("MoveY", inputConfig.lerpedKeyboardY);
        playerAnimator.SetFloat("MoveX", inputConfig.lerpedkeyboardX);
    }

    public void ChangPlayerAimState(bool check)
    {
        playerAnimator.SetBool("isAiming", check);
    }
}
