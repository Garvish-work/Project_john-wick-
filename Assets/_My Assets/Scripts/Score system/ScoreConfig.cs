using UnityEngine;

[CreateAssetMenu (fileName = "Score config", menuName = "Scriptable/Score config")]
public class ScoreConfig : ScriptableObject
{
    public int currentScore = 0;
    public int highscore = 0;
    public int zombieRemaining = 0;

    [Space]
    public int deathScore = 1000;
    public int headScore = 300;
    public int chestScore = 10;
    public int handScore = 5;
    public int legScore = 2;
}
