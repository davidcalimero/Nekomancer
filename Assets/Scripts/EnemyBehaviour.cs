using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float healthPoints = 20;
    public float speed = 1.0f;

    public float damage = 5.0f;
    public float rateDamage = 1.0f;

    public int indexLane;
    public RectTransform initPos;
    public RectTransform endPos;

    private bool _isAttacking = false;
    private float _variation = 1.0f;
    private float _variationNextEnemy = 15.0f;

    private float _lastDamage = 0;

    private GameObject _evocationAttacking = null;
    private float _attackVariation = 0;

    public void Start()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos.anchoredPosition;

        _variation = Random.Range(0.7f, 0.9f);
        _variationNextEnemy = Random.Range(10, 45);

        _lastDamage = Time.time;
        _attackVariation = Random.Range(0.2f, 0.9f);
    }

    public void Update()
    {
        if (_isAttacking)
        {
            if(Time.time - _lastDamage > rateDamage + _attackVariation)
            {
                _evocationAttacking.GetComponent<EvocationController>().healthPoints -= damage;
                if (_evocationAttacking.GetComponent<EvocationController>().healthPoints <= 0)
                {
                    _isAttacking = false;
                    DestroyImmediate(_evocationAttacking);
                }
                _lastDamage = Time.time;
            }
        }
        else if (Vector2.Distance(transform.GetComponent<RectTransform>().anchoredPosition, endPos.anchoredPosition) > 0.1f)
        {
            if(transform.GetSiblingIndex() < transform.parent.childCount - 1)
            {
                if(Vector2.Distance(transform.GetComponent<RectTransform>().anchoredPosition, transform.parent.GetChild(transform.GetSiblingIndex()+1).GetComponent<RectTransform>().anchoredPosition) < _variationNextEnemy)
                {
                    return;
                }
            }

            transform.GetComponent<RectTransform>().anchoredPosition += (endPos.anchoredPosition - transform.GetComponent<RectTransform>().anchoredPosition).normalized * 50 * _variation * speed * Time.deltaTime;
        }
        else
        {
            FindObjectOfType<EnemyWaveController>().UpdateDeletedEnemy(indexLane);

            Destroy(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Evocation"))
        {
            _isAttacking = true;
            _evocationAttacking = collision.gameObject;
        }
        if (collision.tag.Equals("BulletNormal"))
        {
            healthPoints -= collision.gameObject.GetComponent<BulletController>().damage;

            Destroy(collision.gameObject);

            if(healthPoints <= 0)
                Destroy(gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {

    }

}
