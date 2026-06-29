using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBarSystem : MonoBehaviour
{
    [SerializeField] GameObject mainHolder;
    [SerializeField] private float timer = 0;
    [SerializeField] private float maxShowcaseTime = 4;
    [SerializeField] private Slider healthBar;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, 0);

    Vector3 uiPosition;
    Transform enemyTransform;

    private void OnEnable()
    {
        EnemyActions.EnemyGotHit += UpdateSlider;
    }

    private void OnDisable()
    {
        EnemyActions.EnemyGotHit -= UpdateSlider;
    }

    public void UpdateSlider(float _value, Transform _enemyTransform)
    {
        enemyTransform = _enemyTransform;
        healthBar.value = _value;
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
