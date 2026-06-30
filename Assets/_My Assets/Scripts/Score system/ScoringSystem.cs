using TMPro;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{
    [SerializeField] private ScoreConfig scoreConfig;
    [SerializeField] private TMP_Text scoreCountText;

    private void OnEnable()
    {
        ScoreActions.EnemyGotHit += OnEnemyGotHit;
        ScoreActions.EnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        ScoreActions.EnemyGotHit -= OnEnemyGotHit;
        ScoreActions.EnemyDied -= OnEnemyDied;
    }

    private void Awake()
    {
        scoreConfig.currentScore = 0;
        UpdateScoreUi();
    }

    private void UpdateScoreUi()
    {
        scoreCountText.text = scoreConfig.currentScore.ToString("00000");
    }

    private void OnEnemyGotHit(BodyPart bodyPart)
    {
        switch (bodyPart)
        {
            case BodyPart.HEAD:
                scoreConfig.currentScore += scoreConfig.headScore;
                break;
            case BodyPart.HANDS:
                scoreConfig.currentScore += scoreConfig.handScore;
                break;
            case BodyPart.LEGS:
                scoreConfig.currentScore += scoreConfig.legScore;
                break;
            case BodyPart.CHEST:
                scoreConfig.currentScore += scoreConfig.chestScore;
                break;
        }
        UpdateScoreUi();
    }

    private void OnEnemyDied()
    {
        scoreConfig.currentScore += scoreConfig.deathScore;
        UpdateScoreUi();
    }
}
