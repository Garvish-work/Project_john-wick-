using UnityEngine;

public class WeaponHandgun : BaseWeapon
{
    [SerializeField] private Transform mainHolder;
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private Animator weaponAnimator;
    [SerializeField] private ParticleSystem[] weaponFx;
    [SerializeField] private ParticleSystem impackFx;
    [SerializeField] private ParticleSystem bloodFx;
    [SerializeField] private float damageGiven = 15;

    [SerializeField] private LayerMask levelLayerMask;
    [SerializeField] private Camera cam;

    private void Awake()
    {
        mainHolder = transform.parent;  
    }

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
        if (Physics.Raycast(ray, out RaycastHit hit, 100))
        {
            if (hit.collider.TryGetComponent(out IDamagable damagableObject))
            {
                damagableObject.Damage(damageGiven);
                bloodFx.transform.position = hit.point;
                bloodFx.Play(true);
            }
            else if (hit.collider.TryGetComponent(out ICollectables collectable)) collectable.Collect();

            if (hit.collider.gameObject.layer == 6)
            {
                impackFx.transform.position = hit.point;
                impackFx.Play(true);
            }
        }
    }
}

