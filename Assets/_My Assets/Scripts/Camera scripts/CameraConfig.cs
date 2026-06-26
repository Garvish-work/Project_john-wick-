using UnityEngine;

[CreateAssetMenu (fileName = "Camera config", menuName = "Scriptable/Camera config")]
public class CameraConfig : ScriptableObject
{
    public float normalCameraFov = 70;
    public float aimcameraFov = 60;
    public float fovChangeSpeed = 5;
}
