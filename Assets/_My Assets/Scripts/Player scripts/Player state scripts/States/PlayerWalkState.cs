using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
    {

    }

    public override void Enter()
    {
        inputConfig.canShoot = false;
        playerAnimationController.ChangPlayerAimState(false);
        base.Enter();
    }

    public override void FixedUpdate()
    {
        playerMovementController.MovePlayer();
        cameraController.CameraHandling();
        cameraController.UpdateShoulderSide();
    }

    public override void Update()
    {
        playerAnimationController.UpdateAnimation();

        if (inputConfig.isSprinting)
        {
            nextState = new PlayerSprintingState(inputConfig, playerMovementController, playerAnimationController, cameraController);
            Exit();
            return;
        }

        if (!inputConfig.isMoving)
        {
            if (inputConfig.isAiming)
            {
                nextState = new PlayerAimIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
                Exit();
                return;
            }
            else
            {
                nextState = new PlayerIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
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
