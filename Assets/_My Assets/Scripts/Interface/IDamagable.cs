using UnityEngine;

public interface IDamagable
{
    public void Damage(float damageTaken);

    public void Death();
}