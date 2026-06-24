using UnityEngine;

public class WeaponHandgun : BaseWeapon
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private ParticleSystem[] weaponFx;
    [SerializeField] private ParticleSystem impackFx;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Camera cam;

    public override void Shoot()
    {
        weaponAnimator.SetTrigger("Shoot");
        foreach (ParticleSystem fx in weaponFx)
        {
            WeaponActions.WeaponShot?.Invoke();
            fx.Play();
        }
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        Vector3 endPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
        {
            impackFx.transform.position = hit.point;
            impackFx.Play(true);
        }
    }
}

