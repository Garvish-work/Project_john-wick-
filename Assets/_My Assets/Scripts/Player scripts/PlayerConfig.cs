using UnityEngine;

[CreateAssetMenu (fileName = "Player config", menuName = "Scriptable/Player config")]
public class PlayerConfig : ScriptableObject
{
    public bool isAiming = false;
    public bool isHoldingTrigger = false;
    public float playerAimSpeed = 3;
    public float playerNormalSpeed = 4;
}
