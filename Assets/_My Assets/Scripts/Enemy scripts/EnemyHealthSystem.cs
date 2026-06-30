using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    EnemyMovmentSystem enemyMovmentSystem;

    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Transform mainPlayer;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody[] chracterBones;

    bool isDead = false;

    private void Awake()
    {
        health = maxHealth;
        enemyMovmentSystem = GetComponent<EnemyMovmentSystem>();    
        SetRagdoll(false);
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float _health)
    {
        health = _health;
        EnemyActions.EnemyGotHit?.Invoke(health, maxHealth, transform);

        if (_health <= 0)
        {
            isDead = true;
            enemyMovmentSystem.SetMovmentOff();
            SetRagdoll(true);
        }
    }

    public Transform GetPlayerTransform()
    {
        return mainPlayer;
    }

    private void SetRagdoll(bool check)
    {
        switch (check)
        {
            case false:
                enemyAnimator.enabled = true;
                foreach (Rigidbody bone in chracterBones)
                {
                    bone.isKinematic = true;
                }
                break;

            case true:
                enemyAnimator.enabled = false;
                foreach (Rigidbody bone in chracterBones)
                {
                    bone.isKinematic = false;
                }
                break;
        }
    }
}

