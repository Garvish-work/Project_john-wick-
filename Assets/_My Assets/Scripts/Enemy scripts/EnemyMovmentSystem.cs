using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovmentSystem : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private ZombieType zombieType;
    CharacterJoint joint;

    bool isDead = false;

    private void Awake()
    {
        gameManager = GameManager.instance;
        target = gameManager.GetMainPlayer();
    }

    private void OnEnable()
    {
        isDead = false;
        agent.enabled = true;
        ZombieSetup();
    }

    private void OnDisable()
    {
        enemyAnimator.enabled = true;  
    }

    private void ZombieSetup()
    {
        StartCoroutine(nameof(Movement));

        switch (zombieType)
        {
            case ZombieType.WALKING:
                agent.speed = .5f;

                enemyAnimator.Play("Walk", 0, Random.Range(0f, 1f));
                enemyAnimator.Update(0f);
                break;
            case ZombieType.RUNNING:
                agent.speed = 2f;
                enemyAnimator.SetTrigger("Run");

                enemyAnimator.Play("Run", 0, Random.Range(0f, 1f));
                enemyAnimator.Update(0f);
                break;
            case ZombieType.CRAWLING:
                agent.speed = .6f;
                enemyAnimator.SetTrigger("Crawl");

                enemyAnimator.Play("Crawl", 0, Random.Range(0f, 1f));
                enemyAnimator.Update(0f);
                break;
        }
    }

    private IEnumerator Movement()
    {
        WaitForSeconds wait = new WaitForSeconds(.8f);

        while (!isDead)
        {
            agent.SetDestination(target.position);
            yield return wait;
        }
    }

    public void SetMovmentOff()
    {
        isDead = true;
        agent.enabled = false;
    }
}
public enum ZombieType
{
    WALKING,
    RUNNING,
    CRAWLING
}