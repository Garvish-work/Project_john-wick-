using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager instnace;

    [SerializeField] private InputConfig inputConfig;
    [SerializeField] private PlayerMovementController playerMovementController;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerAnimationController playerAnimationController;   
    PlayerBaseState playerCurrentState;

    private void Awake()
    {
        instnace = this;
    }

    private void Start()
    {
        playerCurrentState = new PlayerIdleState(inputConfig, playerMovementController, playerAnimationController, cameraController);
    }

    private void Update()
    {
        playerCurrentState = playerCurrentState.Process();
    }

    private void FixedUpdate()
    {
        playerCurrentState.FixedUpdate(); 
    }
}