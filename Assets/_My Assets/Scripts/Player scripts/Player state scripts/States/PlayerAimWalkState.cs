using UnityEngine;

public class PlayerAimWalkState : PlayerBaseState
{
    public PlayerAimWalkState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        playerAnimationController.UpdateAnimation();

        playerMovementController.MovePlayer();
        playerMovementController.CheckForFootSteps();

        cameraController.CameraHandling();
        cameraController.UpdateShoulderSide();
        if (!inputConfig.isMoving)
        {
            nextState = new PlayerIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
            Exit();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
