using UnityEngine;

public class Tower : MonoBehaviour
{
    // TODO: Improve tower placement by showing the range before placing a tower

    private Transform target;
    private Enemy targetEnemy;
    private float fireCountdown = 0f;

    [Header("General")]
    public float range = 10f;

    [Header("Projectiles (default)")]
    public float fireRate = 1f;
    public GameObject bulletPrefab;

    [Header("Beams")]
    public bool UseBeam = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 30;
    public float slowPercentage = .5f;

    [Header("Setup")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform pivotPoint;
    public Transform firePoint;

    private void Start()
    {
        // trigger the update target method twice a second starting immediately
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            // shut off the laser when we lose our target
            if (UseBeam && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                impactLight.enabled = false;
                impactEffect.Stop();
            }

            return;
        }

        lockOnTarget();
        fireOnTarget();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void UpdateTarget()
    {
        // find closest target
        var enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        var shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        // TODO: add logic to not just find the nearest enemy, but the one that is furthest on the waypoint path

        foreach(var enemy in enemies)
        {
            var distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        // check if we have a closest target and it's in range
        target = nearestEnemy != null && shortestDistance <= range
            ? nearestEnemy.transform
            : null;

        targetEnemy = target.GetComponent<Enemy>();
    }

    private void lockOnTarget()
    {
        // find the target direction, calculate the correct rotation to the target,
        // and rotate the turret's pivot point using only the y-axis
        // use lerp to make the rotation more gradual
        var targetDirection = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(targetDirection);
        var rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void fireOnTarget()
    {
        if (UseBeam)
        {
            Laser();
        }
        else
        {
            // if we are able to fire, do so
            if (fireCountdown <= 0)
            {
                Shoot();
                fireCountdown = 1 / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        // instantiate bullet
        var bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletGameObject.GetComponent<Bullet>();

        // set target for bullet
        bullet?.Seek(target);
    }

    private void Laser()
    {
        // fire ze lasers!
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactLight.enabled = true;
            impactEffect.Play();
        }

        // apply laser damage
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPercentage);

        // track the target with our laser
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        // track the target with the impact effect
        var impactEffectDirection = firePoint.position - target.position;
        impactEffect.transform.rotation = Quaternion.LookRotation(impactEffectDirection);
        impactEffect.transform.position = target.position + impactEffectDirection.normalized;
    }
}
