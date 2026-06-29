using UnityEngine;

public class EnemyBodyPartSystem : MonoBehaviour, IDamagable
{
    [SerializeField] private Animator enemyAnimatior;
    [SerializeField] private EnemyHealthSystem healthSystem;
    [SerializeField] private BodyPart bodyPart;

    [Space]
    [SerializeField] private string dealthType;
    [SerializeField] private float damageMultiplier = 1;

    public void Damage(float damageTaken)
    {
        float health = healthSystem.GetHealth();
        health = Mathf.Max(0, health -= (damageTaken * damageMultiplier));

        if (health <= 0) Death();
        else enemyAnimatior.SetTrigger("Hit");

        healthSystem.SetHealth(health);
    }

    public void Death()
    {
        enemyAnimatior.SetTrigger(dealthType);
    }
}

public enum BodyPart
{
    HEAD,
    HANDS,
    LEGS,
    CHEST,
    NULL
}
