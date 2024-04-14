using UnityEngine;
using System.Collections;

public class SummonerController : MonoBehaviour
{
    public Animator animator;

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
