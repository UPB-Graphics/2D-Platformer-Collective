using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayer;
    public GameObject currentSword; //assume there are multiple types of swords each with its own stats

    float nextAttackTime;

    public int attackModifier = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
                nextAttackTime = Time.time + 1f / currentSword.GetComponent<WeaponStats>().attackRate;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        GetComponent<Stats>().attack.currentHealth -= damage;

        animator.SetTrigger("hurt");

        if (GetComponent<Stats>().attackcurrentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("IsDead", true);
    }

    void Attack()
    {
        //Play attack animation
        animator.SetTrigger("Attack");
        //Detect enemies hit by attack
        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, currentSword.GetComponent<WeaponStats>().attackRange, enemyLayer);

        //Apply damage to enemies
        foreach(Collider2D enemy in enemiesHit)
        {
            Debug.Log("we hit" + enemy.name);
            enemy.GetComponent<EnemyCombat>().TakeDamage(DamageFormula(enemy.GetComponent<EnemyCombat>()));
        }
    }

    public void SetAttackModifier(int modifier)
    {
        attackModifier = modifier;
    }

    int DamageFormula(EnemyCombat enemy)
    {
        int damage = 5 * GetComponent<Stats>().attack * currentSword.GetComponent<WeaponStats>().attackPower * attackModifier / enemy.defense;
        return damage;
    }
}
