using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyMovmentSystem : MonoBehaviour
{
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private Transform target;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private ZombieType zombieType;

    bool isDead = false;

    private void Awake()
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
        while (!isDead)
        {
            agent.SetDestination(target.position);  
            yield return null;
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