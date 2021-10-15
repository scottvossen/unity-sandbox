using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    // TODO: Improve tower placement by showing the range before placing a tower

    private Transform target;
    private float fireCountdown = 0f;

    [Header("Attributes")]
    public float range = 10f;
    public float fireRate = 1f;

    [Header("Setup")]
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;
    public Transform pivotPoint;
    public GameObject bulletPrefab;
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
            return;
        }

        rotateToTarget();

        // if we are able to fire, do so
        if (fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1 / fireRate;
        }

        fireCountdown -= Time.deltaTime;
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
    }

    private void rotateToTarget()
    {
        // find the target direction, calculate the correct rotation to the target,
        // and rotate the turret's pivot point using only the y-axis
        // use lerp to make the rotation more gradual
        var targetDirection = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(targetDirection);
        var rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        // instantiate bullet
        var bulletGameObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        var bullet = bulletGameObject.GetComponent<Bullet>();

        // set target for bullet
        bullet?.Seek(target);
    }
}
