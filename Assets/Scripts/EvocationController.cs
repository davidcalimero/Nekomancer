using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvocationController : MonoBehaviour
{
    public float healthPoints = 50;
    public float maxHealthPoints = 50;

    public float cooldownFire = 1;

    public GameObject bulletPrefab;

    private float _lastFire;
    private float _variationFire = 0;

    public void Start()
    {
        _lastFire = Time.time;
    }

    public void AttackEnemy()
    {
        if (Time.time - _lastFire > cooldownFire + _variationFire)
        {
            GameObject _bullet = Instantiate(bulletPrefab, transform.parent.parent);
            _bullet.transform.position = transform.position;

            _lastFire = Time.time;
            _variationFire = Random.Range(.2f, .6f);
        }
    }

    public void DamageFromEnemy(float damage)
    {
        healthPoints -= damage;
        if(healthPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (IsReadyToAttack()) AttackEnemy();
    }

    private bool IsReadyToAttack()
    {
        if(transform.parent.parent.GetChild(9).transform.childCount > 2)
        {
            if(Vector3.Distance(transform.position, transform.parent.parent.GetChild(9).transform.GetChild(transform.parent.parent.GetChild(9).transform.childCount - 1).position) < 400)
            {
                return true;
            }
        }
        return false;
    }
}
