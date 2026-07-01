using UnityEngine;
using System.Collections;

public class EnemyHealthSystem : MonoBehaviour
{
    GameManager gameManager;
    EnemyMovmentSystem enemyMovmentSystem;
    EnemySpawnSystem enemySpawnSystem;

    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Transform mainPlayer;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Rigidbody[] chracterBones;
    [SerializeField] private GameObject miniMapMarker;

    bool isDead = false;

    private void Awake()
    {
        enemySpawnSystem = EnemySpawnSystem.instance; 
        gameManager = GameManager.instance;
        mainPlayer = gameManager.GetMainPlayer();
        enemyMovmentSystem = GetComponent<EnemyMovmentSystem>();

        health = maxHealth;
        SetRagdoll(false);
        isDead = false;
    }

    private void OnEnable()
    {
        isDead = false;
        health = maxHealth;
        SetRagdoll(false);
        miniMapMarker.SetActive(true);
    }

    private void OnDisable()
    {
        SetRagdoll(false);
    }

    public void SetMainPlayer(Transform _mainPlayer)
    {
        mainPlayer = _mainPlayer;
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
            if (isDead) return;

            StartCoroutine(nameof(AfterDead));

            miniMapMarker.SetActive(false);
            enemyMovmentSystem.SetMovmentOff();
            SetRagdoll(true);

            ScoreActions.EnemyDied?.Invoke();
            isDead = true;
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
                    bone.linearVelocity = Vector3.zero;
                    bone.angularVelocity = Vector3.zero;
                    bone.Sleep();
                }
                break;

            case true:
                enemyAnimator.enabled = false;
                foreach (Rigidbody bone in chracterBones)
                {
                    bone.isKinematic = false;
                    bone.linearVelocity = Vector3.zero;     
                    bone.angularVelocity = Vector3.zero;
                }
                break;
        }
    }

    Vector3 tempPosition = Vector3.up * -10;
    IEnumerator AfterDead()
    {
        float timer = 5;
        while (timer > 2)
        {
            timer -= Time.deltaTime;
            yield return null;  
        }

        transform.position = tempPosition;
        SetRagdoll(false);
        enemyAnimator.enabled= true;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        enemySpawnSystem.DeactiveEnemies(gameObject);
    }
}

