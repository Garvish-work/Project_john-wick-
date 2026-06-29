using UnityEngine;

public class PlayerBaseState
{
    public enum EventState
    {
        ENTER, UPDATE, EXIT
    };
    protected EventState eventState;

    protected InputConfig inputConfig;
    protected Animator playerAnimator;

    protected CameraController cameraController;
    protected PlayerMovementController playerMovementController;
    protected PlayerAnimationController playerAnimationController;

    protected PlayerBaseState nextState;

    public PlayerBaseState(InputConfig _inputConfig, PlayerMovementController _playerMovementController, PlayerAnimationController _playerAnimationController, CameraController _cameraController)
    {
        inputConfig = _inputConfig;
        playerMovementController = _playerMovementController;
        cameraController = _cameraController;
        playerAnimationController = _playerAnimationController; 

        eventState = EventState.ENTER;
    }

    public virtual void Enter() { eventState = EventState.UPDATE; }
    public virtual void Update() { eventState = EventState.UPDATE; }
    public virtual void FixedUpdate() { }
    public virtual void Exit()
    {
        eventState = EventState.EXIT;
    }

    public PlayerBaseState Process()
    {
        if (eventState == EventState.ENTER) { Enter(); }
        if (eventState == EventState.UPDATE) { Update(); }
        if (eventState == EventState.EXIT)
        {
            Exit();
            return nextState;
        }
        else return this;
    }

    public void FixedProcess()
    {
        FixedUpdate();
    }
}