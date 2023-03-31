using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //animation och rb
    public Animator animator;
    public Rigidbody2D rigidbody2D;
    //n�gra variablar
    public int maxHP = 100;
    int currentHP;
    

    // Start
    void Start()
    {
        currentHP = maxHP;
    }

    // damage 
    public void TakeDamage(int dmg)
    {
        // tar skada
        currentHP -= dmg;

        animator.SetTrigger("isHurt");
        // ifall hp �r 0 eller mindre s� spelar die()
        if (currentHP <= 0)
        {
            die();
        }
    }

    void die()
    {
        // bara till logg
        Debug.Log("Enemy died");
        // till animator s� animation spelar vid die
        animator.SetBool("isDead", true);

        //disablar n�gra grejer s� den ej har hitbox l�ngre

        //st�nger av hitbox
        GetComponent<Collider2D>().enabled = false;
        // st�nger av enemy scripted
        this.enabled = false;
        // g�r s� att enemy ej faller igenom v�rlden vid die
        rigidbody2D.gravityScale = 0f;

        // fryser x och y axeln
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

        GetComponent<UnityEngine.Rendering.Universal.ShadowCaster2D>().enabled = false;

    }
}
