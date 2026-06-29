using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Animator enemyAnimator;

    public float GetHealth()
    {
        return health;  
    }

    public void SetHealth(float _health)
    {
        health = _health;
        EnemyActions.EnemyGotHit?.Invoke(health, transform);
    }
        
}
