using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
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

        cameraController.CameraHandling();
        cameraController.UpdateShoulderSide();  
        if (inputConfig.isMoving)
        {
            nextState = new PlayerAimWalkState(inputConfig, playerMovementController, playerAnimationController, cameraController);
            Exit();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
