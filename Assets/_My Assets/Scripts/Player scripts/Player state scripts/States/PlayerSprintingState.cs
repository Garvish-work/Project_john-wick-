using UnityEngine;

public class PlayerSprintingState : PlayerBaseState
{
    public PlayerSprintingState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
    {
    }

    public override void Enter()
    {
        inputConfig.canShoot = false;
        playerAnimationController.ChangPlayerAimState(false);
        playerAnimationController.ChangeSprinteState(true);
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

        if (inputConfig.keyboardX != 0 || !inputConfig.isMoving || inputConfig.isAiming)
        {
            inputConfig.isSprinting = false;
        }

        if (!inputConfig.isSprinting)
        {
            playerAnimationController.ChangeSprinteState(false);
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
    }
}
