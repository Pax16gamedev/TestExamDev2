using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float currentHealth;
    public float CurrentHealth => currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (damage <= 0) return;

        print($"Damage received {damage}");

        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
