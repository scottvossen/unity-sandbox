using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
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
    }

    private void HitTarget()
    {
        var effect = Instantiate(impactEffect, transform.position, transform.rotation);

        Destroy(target.gameObject);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
