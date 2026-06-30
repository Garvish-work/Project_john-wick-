using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class HealthBarSystem : MonoBehaviour
{
    [SerializeField] GameObject mainHolder;
    [SerializeField] Animator uiAnimator;
    [SerializeField] TMP_Text scoreText;

    [Space]
    [SerializeField] private float timer = 0;
    [SerializeField] private float maxShowcaseTime = 4;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, 0);

    Vector3 uiPosition;
    Transform enemyTransform;

    private void OnEnable()
    {
        EnemyActions.EnemyGotHit += UpdateSlider;
        ScoreActions.EnemyGotHit += UpdateScore;
    }

    private void OnDisable()
    {
        EnemyActions.EnemyGotHit -= UpdateSlider;
        ScoreActions.EnemyGotHit -= UpdateScore;
    }

    private void UpdateScore(BodyPart bodyPart)
    {
        switch (bodyPart)
        {
            case BodyPart.HEAD:
                scoreText.text = "HEADSHOT";
                break;
            case BodyPart.HANDS:
                scoreText.text = "ARM";
                break;
            case BodyPart.LEGS:
                scoreText.text = "LEGS";
                break;
            case BodyPart.CHEST:
                scoreText.text = "CHEST";
                break;
        }
    }

    public void UpdateSlider(float _health,float _maxHealth, Transform _enemyTransform)
    {
        uiAnimator.SetTrigger("Hit");
        enemyTransform = _enemyTransform;
        healthBar.maxValue = _maxHealth;
        healthBar.value = _health;
        timer = maxShowcaseTime;
        if (!TimerRunning) StartCoroutine(nameof(ShowHealthBar));   
    }

    bool TimerRunning = false;
    private IEnumerator ShowHealthBar()
    {
        TimerRunning = true;
        mainHolder.SetActive(true);
        while (timer > 0 && healthBar.value > 0)
        { 
            uiPosition = Camera.main.WorldToScreenPoint(enemyTransform.position + offset);   
            transform.position = uiPosition;
            timer -= Time.deltaTime;
            yield return null;  
        }
        mainHolder.SetActive(false);
        TimerRunning = false;
    }
}
