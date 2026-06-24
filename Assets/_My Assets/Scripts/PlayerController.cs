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
    }

    private void MovePlayer()
    {
        Vector3 movementVector = (transform.forward * inputConfig.keyboardY) + (transform.right * inputConfig.keyboardX);
        rb.linearVelocity = movementVector.normalized * playerSpeed;
        playerAnimator.SetFloat("MoveY", inputConfig.lerpedKeyboardY);
        playerAnimator.SetFloat("MoveX", inputConfig.lerpedkeyboardX);
    }

    private void OnTriggerPressed(bool check)
    {
        if (check) equippedWeapon.Shoot();
    }
}
