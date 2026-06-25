using TMPro;
using UnityEngine;

public class DemoSceneUiController : MonoBehaviour
{
    [SerializeField] private TMP_Text collectableCountText;
    [SerializeField] private int collectableCount;
    [SerializeField] private int totalCollectable;

    private void OnEnable()
    {
        PlayerActions.CollectableCollected += OnCollectableCollected;
    }

    private void OnDisable()
    {
        PlayerActions.CollectableCollected -= OnCollectableCollected;
    }

    private void OnCollectableCollected()
    {
        collectableCount++;
        if (collectableCount >= totalCollectable) collectableCountText.text = "COMPLETE";
        else collectableCountText.text = $"{collectableCount}/{totalCollectable}";
    }
}
