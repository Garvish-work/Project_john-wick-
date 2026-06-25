using UnityEngine;

[CreateAssetMenu (fileName = "Sfx config", menuName = "Scriptable/Sfx congif")]
public class SfxConfig : ScriptableObject
{
    public AudioClip[] footsteps;
    public AudioClip[] pistolGunshots;
}

public enum SfxType
{
    PISTOL_GUNSHOT,
    FOOTSETPS
}
