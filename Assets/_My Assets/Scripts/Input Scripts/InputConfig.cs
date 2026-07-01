using UnityEngine;

[CreateAssetMenu(fileName = "Input config", menuName = "Scriptable/Input config")]
public class InputConfig : ScriptableObject
{
    [Header ("Player movement")]
    public float keyboardY;
    public float keyboardX;
    public bool isMoving = false;
    public bool isSprinting = false;

    [Space]
    public bool isKicking = false;
    public float kickTimer = 2;

    [Space]
    public float lerpedKeyboardY;
    public float lerpedkeyboardX;

    [Header("Camera control")]
    public bool isAiming = false;
    public ShoulderSide shoulderSide;
    public bool takeInputs;
    public float yaw;
    public float lerpedYaw;
    public float pitch;

    [Space]
    public bool canShoot = false;
    public float mouseSensitivity = 2;
    public float aimDamping = 8;
    public float cameraFarsight = 50f; 
    public Vector3 recoilOffset = new Vector3(0,0,0.3f);
    public float recoilRotationOffset = 2;
}

public enum ShoulderSide
{
    Left,
    Right
}