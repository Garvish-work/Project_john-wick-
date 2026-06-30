using UnityEngine;

public class PlayerKickingState : PlayerBaseState
{
    public PlayerKickingState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
    {

    }

    public override void Enter()
    {
        playerMovementController.AbsoluteZeroMovement();
        playerAnimationController.PlayerKick();
        base.Enter();
    }

    float timer = 0;
    public override void Update()
    {
        timer += Time.deltaTime;    

        if (timer > inputConfig.kickTimer)
        {
            if (inputConfig.isAiming)
                nextState = new PlayerAimIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
            else 
                nextState = new PlayerIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
            Exit();
        }
    }

    public override void FixedUpdate()
    {
        cameraController.CameraHandling();
        cameraController.UpdateShoulderSide();
    }

    public override void Exit()
    {
        inputConfig.isKicking = false;
        base.Exit();
    }
}
