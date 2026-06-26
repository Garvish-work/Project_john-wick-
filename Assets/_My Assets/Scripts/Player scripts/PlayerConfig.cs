using UnityEngine;

[CreateAssetMenu (fileName = "Player config", menuName = "Scriptable/Player config")]
public class PlayerConfig : ScriptableObject
{
    public bool isAiming = false;
    public bool isHoldingTrigger = false;
}
