using UnityEngine;

public class Collectables : MonoBehaviour, ICollectables
{
    [SerializeField] private ParticleSystem collectfx;

    public void Collect()
    {
        Instantiate(collectfx, transform.position, Quaternion.identity);
        Destroy(gameObject);
        PlayerActions.CollectableCollected?.Invoke();
    }
}

public interface ICollectables
{
    public void Collect();
}