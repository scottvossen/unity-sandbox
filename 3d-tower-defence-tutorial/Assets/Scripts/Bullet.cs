using UnityEngine;
using System.Collections;
using System.Linq;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public int damage = 50;
    public float speed = 70f;
    public float explosionRadius = 0f;
    public GameObject impactEffect;
    
    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        var targetDirection = target.position - transform.position;
        var distanceForFrame = speed * Time.deltaTime;

        // stop moving if we're going to hit the target
        if (targetDirection.magnitude <= distanceForFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(targetDirection.normalized * distanceForFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        var effect = Instantiate(impactEffect, transform.position, transform.rotation);

        if (explosionRadius > 0)
        {
            Explode();
        } else
        {
            DamageTarget(target);
        }

        // clean up this bullet and it's artifacts
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    private void Explode()
    {
        var itemsInRange = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (var item in itemsInRange)
        {
            if (item.tag == "Enemy")
            {
                DamageTarget(item.transform);
            }
        }
    }

    private void DamageTarget(Transform enemyTransform)
    {
        var enemy = enemyTransform.GetComponent<Enemy>();

        if (enemy == null)
        {
            Debug.LogError("No enemy component found.");
        }

        enemy?.TakeDamage(damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
