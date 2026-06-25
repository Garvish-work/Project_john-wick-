using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private InputConfig inputConfig;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private BaseWeapon equippedWeapon;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float playerSpeed = 3;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.2f;

        rb = GetComponent<Rigidbody>(); 
    }

    private void OnEnable()
    {
        PlayerActions.TriggerPressed += OnTriggerPressed;
    }

    private void OnDisable()
    {
        PlayerActions.TriggerPressed += OnTriggerPressed;
    }

    private void Update()
    {
        MovePlayer();
        CheckForFootSteps();
    }

    private void MovePlayer()
    {
        float yVelocity = 0;    
        Vector3 movementVector = (transform.forward * inputConfig.keyboardY) + (transform.right * inputConfig.keyboardX);
        movementVector = movementVector.normalized;
        yVelocity = rb.linearVelocity.y;
        rb.linearVelocity = movementVector * playerSpeed + transform.up * yVelocity;
        playerAnimator.SetFloat("MoveY", inputConfig.lerpedKeyboardY);
        playerAnimator.SetFloat("MoveX", inputConfig.lerpedkeyboardX);
    }

    private void OnTriggerPressed(bool check)
    {
        if (check) equippedWeapon.Shoot();
    }

    float lastFootstep = 0;
    private void CheckForFootSteps()
    {
        float footstep = playerAnimator.GetFloat("Footstep");
        if (lastFootstep > 0.5f && footstep < 0.5f || lastFootstep < 0.5f && footstep > 0.5f)
        {
            Debug.Log("Play footstep sound");
            SfxActions.PlayerSfx?.Invoke(SfxType.FOOTSETPS);
        }
        lastFootstep = footstep;
    }
}
