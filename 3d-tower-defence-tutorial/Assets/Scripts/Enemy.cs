using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform target;
    private int waypointIndex;

    public float speed = 10f;
    public int health = 100;
    public int value = 50;
    public GameObject deathEffect;

    public void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Start()
    {
        target = Waypoints.waypoints[0];
    }


    private void Update()
    {
        MoveToWaypoint();

        // if we're at our waypoint, continue on to the next waypoint
        if (Vector3.Distance(transform.position, target.position) < 0.4f)
        {
            TargetNextWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        var direction = target.position - transform.position;

        // move at set speed including deta time to adjust for frame rate differences
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
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
    }

    private void Die()
    {
        // give the player credit
        PlayerStats.Money += value;

        // apply death effect
        var effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        // destroy object
        Destroy(gameObject);
    }
}
