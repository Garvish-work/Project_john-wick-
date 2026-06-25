using UnityEngine;

public class WeaponHandgun : BaseWeapon
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private ParticleSystem[] weaponFx;
    [SerializeField] private ParticleSystem impackFx;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Camera cam;

    public override void Shoot()
    {
        weaponAnimator.SetTrigger("Shoot");
        SfxActions.PlayerSfx?.Invoke(SfxType.PISTOL_GUNSHOT);
        foreach (ParticleSystem fx in weaponFx)
        {
            WeaponActions.WeaponShot?.Invoke();
            fx.Play();
        }
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, 100, layerMask))
        {
            if (hit.collider.TryGetComponent(out ICollectables c)) c.Collect();
            impackFx.transform.position = hit.point;
            impackFx.Play(true);
        }
    }
}

