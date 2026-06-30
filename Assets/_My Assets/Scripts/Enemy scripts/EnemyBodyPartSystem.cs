using UnityEngine;

public class EnemyBodyPartSystem : MonoBehaviour, IDamagable
{
    Rigidbody bone;
    [SerializeField] private Animator enemyAnimatior;
    [SerializeField] private EnemyHealthSystem healthSystem;
    [SerializeField] private BodyPart bodyPart;

    [Space]
    [SerializeField] private string dealthType;
    [SerializeField] private float damageMultiplier = 1;

    private void Awake()
    {
        bone = GetComponent<Rigidbody>();   
    }

    public void Damage(float damageTaken)
    {
        float health = healthSystem.GetHealth();
        health = Mathf.Max(0, health -= (damageTaken * damageMultiplier));

        healthSystem.SetHealth(health);

        if (health <= 0) Death();
        else enemyAnimatior.SetTrigger("Hit");
    }

    public void Death()
    {
        enemyAnimatior.enabled = false;
        bone.AddForce(healthSystem.GetPlayerTransform().transform.forward * 25 , ForceMode.Impulse);
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
