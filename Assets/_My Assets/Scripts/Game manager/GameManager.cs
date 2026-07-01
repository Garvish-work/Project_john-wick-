using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Transform mainPlayer;

    private void Awake()
    {
        instance = this;
    }

    public Transform GetMainPlayer()
    {
        return mainPlayer;
    }
}
