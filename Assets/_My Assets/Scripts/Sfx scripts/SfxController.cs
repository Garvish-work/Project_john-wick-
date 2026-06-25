using UnityEngine;

public class SfxController : MonoBehaviour
{
    [SerializeField] private SfxConfig sfxConfig;
    [SerializeField] private AudioSource weaponAudioSource;
    [SerializeField] private AudioSource footstepsAudioSource;

    private void OnEnable()
    {
        SfxActions.PlayerSfx += OnPlaySfx;
    }

    private void OnDisable()
    {
        SfxActions.PlayerSfx -= OnPlaySfx;
    }

    private void OnPlaySfx(SfxType sfxType)
    {
        int count = 0;
        switch (sfxType)
        {
            case SfxType.PISTOL_GUNSHOT:
                count = sfxConfig.pistolGunshots.Length;
                weaponAudioSource.PlayOneShot(sfxConfig.pistolGunshots[Random.Range(0, count)]);
                break;
            case SfxType.FOOTSETPS:
                count = sfxConfig.footsteps.Length;
                weaponAudioSource.PlayOneShot(sfxConfig.footsteps[Random.Range(0, count)]);
                break;
        }
    }
}
