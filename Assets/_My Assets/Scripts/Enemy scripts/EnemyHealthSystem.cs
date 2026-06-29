using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Transform healthBarHolder;

    public float GetHealth()
    {
        return health;  
    }

    public void SetHealth(float _health)
    {
        health = _health;   
    }
        
}
