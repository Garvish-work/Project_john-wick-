using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraController: MonoBehaviour
{
    [SerializeField] private InputConfig inputConfig;

    [Space]
    [SerializeField] private Transform characterModel;

    [Space]
    [SerializeField] private Transform tppCamera;
    [SerializeField] private Transform cameraYaw;
    [SerializeField] private Transform cameraPitch;

    [Space]
    [SerializeField] private Transform aimComponent;
    [SerializeField] private Transform recoilComponent;

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
        CameraHandling();
        SetAim();
        UpdateShoulderSide();
    }

    private void CameraHandling()
    {
        characterModel.rotation = Quaternion.Euler(0, inputConfig.lerpedYaw, 0);
        cameraYaw.rotation = Quaternion.Euler(0, inputConfig.yaw, 0);
        cameraPitch.localRotation = Quaternion.Euler(inputConfig.pitch, 0, 0);
    }

    private void SetAim()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 endPoint;

        endPoint = ray.origin + ray.direction * inputConfig.cameraFarsight ; // No hit, point 100 units ahead
        aimComponent.position = Vector3.Lerp(aimComponent.position, endPoint, inputConfig.aimDamping * Time.deltaTime);  
    }

    bool recoilComponemtReset = true;
    private void OnWeaponShot()
    {
        recoilComponent.localPosition = inputConfig.recoilOffset;
        if (recoilComponemtReset) StartCoroutine(ResetRecoilComponent());   
    }

    private IEnumerator ResetRecoilComponent()
    {
        recoilComponemtReset = false;
        while (recoilComponent.localPosition != Vector3.zero)
        {
            recoilComponent.localPosition = Vector3.MoveTowards(recoilComponent.localPosition, Vector3.zero, .5f * Time.deltaTime);
            yield return null;
            recoilComponemtReset = true;
        }
    }

    private void UpdateShoulderSide()
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
