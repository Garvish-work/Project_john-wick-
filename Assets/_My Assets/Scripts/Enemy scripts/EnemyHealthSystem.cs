using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private Transform mainPlayer;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody[] chracterBones;

    private void Awake()
    {
        SetRagdoll(false);
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetHealth(float _health)
    {
        health = _health;
        EnemyActions.EnemyGotHit?.Invoke(health, transform);

        if (_health <= 0) SetRagdoll(true);
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
