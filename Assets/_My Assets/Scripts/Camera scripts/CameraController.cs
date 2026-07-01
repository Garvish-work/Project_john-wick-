using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraController: MonoBehaviour
{
    [SerializeField] private CameraConfig cameraConfig;
    [SerializeField] private InputConfig inputConfig;

    [Space]
    [SerializeField] private Transform characterModel;

    [Space]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform tppCamera;
    [SerializeField] private Transform cameraYaw;
    [SerializeField] private Transform cameraPitch;

    [Space]
    [SerializeField] private Transform aimComponent;
    [SerializeField] private Transform recoilComponent;
    [SerializeField] private Rig aimRig;

    [Header("Change cover system")]
    [SerializeField] private Transform rightSight;
    [SerializeField] private MultiAimConstraint rightConstraint;
    [SerializeField] private Transform leftSight;
    [SerializeField] private MultiAimConstraint leftConstraint;

    private void OnEnable()
    {
        WeaponActions.WeaponShot += OnWeaponShot;
    }

    private void OnDisable()
    {
        WeaponActions.WeaponShot -= OnWeaponShot;
    }
    private void Update()
    {
        SetAim();
        UpdateCameraFov();
    }

    private void FixedUpdate()
    {
        ResetRecoilComponent();
    }

    float targetFov = 0;
    private void UpdateCameraFov() => mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, targetFov, cameraConfig.fovChangeSpeed * Time.deltaTime);

    public void CameraHandling()
    {
        characterModel.rotation = Quaternion.Euler(0, inputConfig.lerpedYaw, 0);
        cameraYaw.rotation = Quaternion.Euler(0, inputConfig.yaw, 0);
        cameraPitch.localRotation = Quaternion.Euler(inputConfig.pitch, 0, 0);
    }

    private void SetAim()
    {
        if (inputConfig.canShoot)
        {
            aimRig.weight = 1;
            targetFov = cameraConfig.aimcameraFov;
        }
        else
        { 
            aimRig.weight = 0;
            targetFov = cameraConfig.normalCameraFov;
        }

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 endPoint;

        endPoint = ray.origin + ray.direction * inputConfig.cameraFarsight ; // No hit, point 100 units ahead
        aimComponent.position = Vector3.Lerp(aimComponent.position, endPoint, inputConfig.aimDamping * Time.deltaTime);  
    }

    bool recoilComponemtReset = true;
    float recoilRotation = 0;
    private void OnWeaponShot()
    {
        recoilComponent.localPosition = inputConfig.recoilOffset;
        recoilRotation = inputConfig.recoilRotationOffset;
    }

    private void ResetRecoilComponent()
    {
        recoilComponent.localPosition = Vector3.MoveTowards(recoilComponent.localPosition, Vector3.zero, 1.3f * Time.deltaTime);
        recoilRotation = Mathf.MoveTowards(recoilRotation, 0, 1.3f * Time.deltaTime);
        recoilComponent.localRotation = Quaternion.Euler(recoilRotation, 0, 0);
    }

    public void UpdateShoulderSide()
    {
        switch (inputConfig.shoulderSide)
        {
            case ShoulderSide.Right:
                tppCamera.position = Vector3.MoveTowards(tppCamera.position, rightSight.position, 9 * Time.deltaTime);
                rightConstraint.weight = .8f;
                leftConstraint.weight = 0;
                break;
            case ShoulderSide.Left:
                tppCamera.position = Vector3.MoveTowards(tppCamera.position, leftSight.position, 9 * Time.deltaTime);
                rightConstraint.weight = 0;
                leftConstraint.weight = 0.8f;
                break;
        }
    }
}
