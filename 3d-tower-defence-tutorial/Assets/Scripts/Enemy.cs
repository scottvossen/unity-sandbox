using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float baseSpeed = 10f;
    [HideInInspector]
    public float speed;
    public float health = 100;
    public int value = 50;
    public GameObject deathEffect;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float slowPercentage)
    {
        var slowedSpeed = baseSpeed * (1f - slowPercentage);

        // only apply the slowed speed if it's worse than our current speed
        // - we may already have stronger debuff applied to our speed
        speed = slowedSpeed < speed ? slowedSpeed : speed;
    }

    private void Start()
    {
        speed = baseSpeed;
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
