using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float maxHealth = 100;
    public float baseSpeed = 10f;
    public int value = 50;
    public GameObject deathEffect;
    public Image healthBar;

    [HideInInspector]
    public float speed;
    
    [HideInInspector]
    public float health;

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.fillAmount = Mathf.Max(0, health / maxHealth);

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
        health = maxHealth;
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
