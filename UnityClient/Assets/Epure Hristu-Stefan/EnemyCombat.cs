using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{

    public Transform attackPoint;
    public Animator animator;
    public LayerMask playerLayer;

    public int attackModifier;
    public float attackRange;
    public int MaxHealth;
    public int attackPower;
    int currentHealth;
    public int defense;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        animator.SetTrigger("hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //enemy AI here
    }

    void Attack()
    {
        //Play attack animation
        animator.SetTrigger("Attack");
        //Detect enemies hit by attack
        Collider2D[] playersHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        //Apply damage to enemies
        foreach (Collider2D player in playersHit)
        {
            Debug.Log("we hit" + player.name);
            player.GetComponent<PlayerCombat>().TakeDamage(DamageFormula(player.GetComponent<PlayerCombat>()));
        }
    }

    public void SetAttackModifier(int modifier)
    {
        attackModifier = modifier;
    }

    int DamageFormula(PlayerCombat player)
    {
        int damage = 5 * attackPower * attackModifier / player.GetComponent<Stats>().defense;
        return damage;
    }
}
