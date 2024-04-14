using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvocationController : MonoBehaviour
{
    public float healthPoints = 50;
    public int distance = 400;
    public float cooldownFire = 1;

    public GameObject bulletPrefab;
    public Animator animator;

    private float _lastFire;
    private float _variationFire = 0;

    public void Start()
    {
        _lastFire = Time.time;
    }

    void SpawnBullet() {
        GameObject _bullet = Instantiate(bulletPrefab, transform.parent.parent);
        Vector3 position = transform.position;
        position.y += 25;
        _bullet.transform.position = position;
        
    }

    void AttackEnemy()
    {
        if (Time.time - _lastFire > cooldownFire + _variationFire)
        {
            if (animator)
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                SpawnBullet();
            }
            
            _lastFire = Time.time;
            _variationFire = Random.Range(.2f, .6f);
        }
    }

    public void DamageFromEnemy(float damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0)
        {
            if(animator)
            {
                animator.SetTrigger("Death");
            }
            else
            {
                Die();
            }
        }
    }

    private void Update()
    {
        if (GameFlow.instance.gameFinished)
        {
            return;
        }

        if (IsReadyToAttack()) AttackEnemy();
    }

    private bool IsReadyToAttack()
    {
        if(transform.parent.parent.GetChild(0).transform.childCount > 2)
        {
            if(Vector3.Distance(transform.position, transform.parent.parent.GetChild(0).transform.GetChild(transform.parent.parent.GetChild(0).transform.childCount - 1).position) < distance)
            {
                return true;
            }
        }
        return false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
