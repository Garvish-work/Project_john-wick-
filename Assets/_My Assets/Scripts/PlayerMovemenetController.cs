using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private InputConfig inputConfig;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private BaseWeapon equippedWeapon;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] private float playerSpeed = 3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.2f;
    }

    private void OnEnable()
    {
        PlayerActions.TriggerPressed += OnTriggerPressed;
    }

    private void OnDisable()
    {
        PlayerActions.TriggerPressed -= OnTriggerPressed;
    }

    public void MovePlayer()
    {
        float yVelocity = 0;
        Vector3 movementVector = (transform.forward * inputConfig.keyboardY) + (transform.right * inputConfig.keyboardX);
        movementVector = movementVector.normalized;
        yVelocity = rb.linearVelocity.y;
        rb.linearVelocity = movementVector * playerSpeed + transform.up * yVelocity;

        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        Debug.DrawRay(transform.position, movementVector * 3, Color.red);

        Debug.Log($"Forward: {transform.forward}, Movement: {movementVector}");
    }

    float lastFootstep = 0;
    public void CheckForFootSteps()
    {
        float footstep = playerAnimator.GetFloat("Footstep");
        if (lastFootstep > 0.5f && footstep < 0.5f || lastFootstep < 0.5f && footstep > 0.5f)
        {
            Debug.Log("Play footstep sound");
            SfxActions.PlayerSfx?.Invoke(SfxType.FOOTSETPS);
        }
        lastFootstep = footstep;
    }
    private void OnTriggerPressed(bool check)
    {
        if (check) equippedWeapon.Shoot();
    }
}
