using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, Iitem
{
    public float newHealth = 100f;
    public void Use(GameObject target)
    {
        if (target.TryGetComponent<LivingEntity>(out LivingEntity health))
        {
            health.RestoreHealth(newHealth);
        }
        Destroy(gameObject);
    }
}
