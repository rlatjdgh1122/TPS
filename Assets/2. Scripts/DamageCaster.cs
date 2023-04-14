using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class DamageCaster : MonoBehaviour
{

    [SerializeField]
    private float casterRadius = 1f;
    [SerializeField]
    private float casterInterpolation = .1f;

    [SerializeField]
    private int monsterDamage = 2;
    [SerializeField]
    public LayerMask whatIsEnemy;
    public void DamageCast()
    {
        RaycastHit hit;
        bool Ishit = Physics.SphereCast(transform.position - transform.forward * casterRadius,
            casterRadius, transform.forward, out hit, casterRadius + casterInterpolation,
            whatIsEnemy);
        if (Ishit)
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable target))
            {
                target.OnDamage(monsterDamage, hit.point, hit.normal);
            }
        }
        else
            Debug.Log("응안맞아~");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, casterRadius);
    }
}
