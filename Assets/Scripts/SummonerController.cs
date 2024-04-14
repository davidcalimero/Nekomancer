using UnityEngine;
using System.Collections;

public class SummonerController : MonoBehaviour
{
    public Animator animator;
    public AudioSource attack;

    public void Attack()
    {
        animator.SetTrigger("Attack");
        attack.Play();
    }

    public void Die()
    {
        animator.SetBool("Dead", true);
    }
}