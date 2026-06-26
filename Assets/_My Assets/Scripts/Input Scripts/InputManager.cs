using System.Xml;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] InputConfig inputConfig;

    private void Awake()
    {
        inputConfig.keyboardY = 0; 
        inputConfig.keyboardX = 0;
        inputConfig.lerpedKeyboardY = 0;
        inputConfig.lerpedkeyboardX = 0;
        inputConfig.isMoving = false;

        inputConfig.yaw = 0;
        inputConfig. lerpedYaw = 0;
        inputConfig.pitch = 0;

        inputConfig.shoulderSide = ShoulderSide.Right;
    }

    private void Update()
    {
        if (inputConfig.takeInputs) MouseInputs();
        KeyboardInputs();
    }

    private void KeyboardInputs()
    {
        inputConfig.keyboardY = Input.GetAxisRaw("Vertical");
        inputConfig.lerpedKeyboardY = Mathf.MoveTowards(inputConfig.lerpedKeyboardY, inputConfig.keyboardY, 5 * Time.deltaTime);
        inputConfig.keyboardX = Input.GetAxisRaw("Horizontal");
        inputConfig.lerpedkeyboardX = Mathf.MoveTowards(inputConfig.lerpedkeyboardX, inputConfig.keyboardX, 5 * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!inputConfig.canShoot)
            {
                if (inputConfig.keyboardX == 0 && inputConfig.keyboardY == 1)
                {
                    inputConfig.isSprinting = !inputConfig.isSprinting;        
                }
            }
        }

        if (inputConfig.keyboardY != 0 || inputConfig.keyboardX != 0) inputConfig.isMoving = true;
        else inputConfig.isMoving = false;

        if (inputConfig.canShoot)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PlayerActions.TriggerPressed(true);
            }
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                PlayerActions.TriggerPressed(false);
            }
        }

        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                inputConfig.isAiming = !inputConfig.isAiming;
            }
        }
    }

    private void MouseInputs()
    {
        inputConfig.yaw += Input.GetAxisRaw("Mouse X");
        inputConfig.lerpedYaw = Mathf.Lerp(inputConfig.lerpedYaw, inputConfig.yaw, (inputConfig.aimDamping + 2) * Time.deltaTime);
        inputConfig.pitch -= Input.GetAxisRaw("Mouse Y");

        if (Input.GetKeyDown(KeyCode.Mouse3))
        {
            if (inputConfig.shoulderSide == ShoulderSide.Left) inputConfig.shoulderSide = ShoulderSide.Right;
            else inputConfig.shoulderSide = ShoulderSide.Left;
        }
    }
}
