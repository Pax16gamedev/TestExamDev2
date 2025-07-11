using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private InputManagerSO inputManager;

    [SerializeField] private float damage = 50;
    [SerializeField] private float cooldown = 2.5f;

    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 3;
    [SerializeField] private LayerMask damageableLayer;

    private Animator animator;
    private float cooldownTimer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        inputManager.SpecialAttackInput += OnSpecialAttackInput;
    }

    private void OnDisable()
    {
        inputManager.SpecialAttackInput -= OnSpecialAttackInput;
    }

    private void OnSpecialAttackInput()
    {
        animator.SetTrigger("SpecialAttack");
    }

    // Llamado desde evento de animacion
    private void SpecialAttack()
    {
        if (cooldownTimer < cooldown) return;
        cooldownTimer = 0;

        print("Attacking!");

        var hits = Physics.OverlapSphere(attackPoint.position, attackRadius, damageableLayer);
        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<HealthSystem>(out var healthSystem))
            {
                healthSystem.TakeDamage(damage);
            }
        }
    }

    private void Start()
    {
        cooldownTimer = cooldown;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
