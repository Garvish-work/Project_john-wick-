using UnityEngine;

public class PlayerAimWalkState : PlayerBaseState
{
    public PlayerAimWalkState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController) : base(_inputConfig, _playerMovementController, _playerAnimationController, _cameraController)
    {

    }

    public override void Enter()
    {
        inputConfig.canShoot = true;
        playerAnimationController.ChangPlayerAimState(true);
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
        playerMovementController.CheckForFootSteps();

        if (inputConfig.isKicking)
        {
            inputConfig.canShoot = false;
            nextState = new PlayerKickingState(inputConfig, playerMovementController, playerAnimationController, cameraController);
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

        if (!inputConfig.isAiming)
        {
            if (inputConfig.isMoving)
            {
                nextState = new PlayerWalkState(inputConfig, playerMovementController, playerAnimationController, cameraController);
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
    }

    public override void Exit()
    {
        Debug.Log("Player aim walk exit");
        base.Exit();
    }
}
