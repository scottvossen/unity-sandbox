using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex;
    private Enemy enemy;

    private void Start()
    {
        target = Waypoints.waypoints[0];
        enemy = GetComponent<Enemy>();
    }


    private void Update()
    {
        MoveToWaypoint();

        // if we're at our waypoint, continue on to the next waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            TargetNextWaypoint();
        }

        // reset our speed in case our last update call had us slowed
        enemy.speed = enemy.baseSpeed;
    }

    private void MoveToWaypoint()
    {
        var direction = target.position - transform.position;

        // move at set speed including deta time to adjust for frame rate differences
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);
    }

    private void TargetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            HandleFinalWaypointReached();
            return;
        }

        // target the next waypoint
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    private void HandleFinalWaypointReached()
    {
        PlayerStats.Lives--;

        Destroy(gameObject);
        WaveSpawner.enemyCount--;
    }
}
