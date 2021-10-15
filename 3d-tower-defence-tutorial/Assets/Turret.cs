using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform target;

    public string enemyTag = "Enemy";
    public float range = 15f;
    public float turnSpeed = 10f;

    public Transform pivotPoint;

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

        // find the target direction, calculate the correct rotation to the target,
        // and rotate the turret's pivot point using only the y-axis
        // use lerp to make the rotation more gradual
        var targetDirection = target.position - transform.position;
        var lookRotation = Quaternion.LookRotation(targetDirection);
        var rotation = Quaternion.Lerp(pivotPoint.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        pivotPoint.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, range);
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
}
