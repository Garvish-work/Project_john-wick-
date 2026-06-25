using UnityEngine;
using System.Collections;

public class CrosshairIdentity : MonoBehaviour
{
    [SerializeField] private Animator crosshairAnimator;
    public float bulletSpread = 0;
    public float spreadPerShot = 0.15f;
    public float returnRate = 1.2f;

    private void OnEnable()
    {
        WeaponActions.WeaponShot += OnWeaponShot;
    }

    private void OnDisable()
    {
        WeaponActions.WeaponShot -= OnWeaponShot;
    }

    private void OnWeaponShot()
    {
        bulletSpread += spreadPerShot;
        if (crosshairToNormal) StartCoroutine(nameof(UpdateWeaponSpread));   
    }

    bool crosshairToNormal = true;
    private IEnumerator UpdateWeaponSpread()
    {
        crosshairToNormal = false;
        while (bulletSpread > 0)
        {
            crosshairAnimator.SetFloat("Aim", bulletSpread);
            bulletSpread -= Time.deltaTime * returnRate;
            yield return null;
        }
        crosshairToNormal = true;
    }
}
