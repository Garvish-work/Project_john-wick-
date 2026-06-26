using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
    {

    }

    public override void Enter()
    {
        inputConfig.canShoot = false;
        playerAnimationController.ChangPlayerAimState(false);
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
            if (inputConfig.isAiming)
            {
                nextState = new PlayerAimWalkState(inputConfig, playerMovementController, playerAnimationController, cameraController);
                Exit();
                return;
            }
            else
            {
                nextState = new PlayerWalkState (inputConfig, playerMovementController, playerAnimationController, cameraController);
                Exit();
                return;
            }
        }

        if (inputConfig.isAiming)
        {
            if (inputConfig.isMoving)
            {
                nextState = new PlayerAimWalkState(inputConfig, playerMovementController, playerAnimationController, cameraController);
                Exit();
                return;
            }
            else
            {
                nextState = new PlayerAimIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
                Exit();
                return;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
