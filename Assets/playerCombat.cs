using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unitygame;

public class playerCombat : MonoBehaviour
{
    public Animator animator;

    public Transform climbAttackPoint;
    public LayerMask enemylayers;

    public int cattackdamage = 40;
    public float cattackRange = 0.5f;
    public float attackr = 2f;
    float nextattacktime = 0f;

    void Update()
    {
        // attack delay på 1 sekund efter attack sker
        if (Time.time >= nextattacktime)
        {
            if (Input.GetKeyDown(KeyCode.V))
            {
                Attack();
                nextattacktime = Time.time + 1f / attackr; // fixed typo
            }
        }
    }

    void Attack()
    {// till animation
        animator.SetTrigger("Attack");

        // detector för enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(climbAttackPoint.position, cattackRange, enemylayers);

        //damage
        foreach (Collider2D enemy in hitEnemies)
        {
            // Check if the enemy exists before trying to damage it
            if (enemy != null && enemy.gameObject.activeSelf)
            {
                enemy.GetComponent<Enemy>().TakeDamage(cattackdamage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {//ritar cirkel för attackrange i unity
        if (climbAttackPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(climbAttackPoint.position, cattackRange);
    }
}
