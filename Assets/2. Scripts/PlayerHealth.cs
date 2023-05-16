using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : LivingEntity
{
    private void OnTriggerEnter(Collider other)
    {
        if (!dead)
        {
            if(other.TryGetComponent<Iitem>(out var item))
            {
                item.Use(gameObject);
                
            }
        }
    }
    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        base.OnDamage(damage, hitPosition, hitNormal);

        CameraAction.Instance.ShakeCamera(5, .6f);


        StartCoroutine(ShowBloodEffect(hitPosition, hitNormal));
        UdateUI();
    }
    private IEnumerator ShowBloodEffect(Vector3 hitPosition, Vector3 hitNormal)
    {
        EffectManager.Instance.PlayHitEffect(hitPosition, hitNormal, transform, EffectManager.EffectType.Flesh);

        yield return new WaitForSeconds(1f);
    }
    private void UdateUI()
    {
        UIManager.Instance.UpdateHealthText(dead ? 0 : health);
    }
    public override void Die()
    {
        base.Die();
    }
    public override void RestoreHealth(float newHealth)
    {
        base.RestoreHealth(newHealth);
        UdateUI();
    }
}
