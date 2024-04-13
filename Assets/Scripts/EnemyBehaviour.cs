using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public float healthPoints = 20;
    public float speed = 1.0f;

    public float damage = 5.0f;
    public float rateDamage = 1.0f;

    public int indexLane;

    public bool hasShield;
    public float shieldHealth = 5;
    public bool hasHeadband1;
    public float headband1Health = 5;
    public bool hasHeadband2;
    public float headband2Health = 5;
    
    public RectTransform initPos;
    public RectTransform endPos;

    private bool _isAttacking = false;
    private float _variation = 1.0f;
    private float _variationNextEnemy = 15.0f;

    private float _lastDamage = 0;

    private GameObject _evocationAttacking = null;
    private float _attackVariation = 0;

    public IEnumerator Start()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos.anchoredPosition;

        _variation = Random.Range(0.7f, 0.9f);
        _variationNextEnemy = Random.Range(10, 45);

        _lastDamage = Time.time;
        _attackVariation = Random.Range(0.2f, 0.9f);

        yield return new WaitForSeconds(.2f);

        transform.GetChild(0).gameObject.SetActive(hasShield);
        transform.GetChild(1).gameObject.SetActive(hasHeadband1);
        transform.GetChild(2).gameObject.SetActive(hasHeadband2);
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
            if (hasShield && shieldHealth > 0)
            {
                shieldHealth -= collision.gameObject.GetComponent<BulletController>().damage;
                if(shieldHealth <= 0) { StartCoroutine(_FallObject(0, transform.GetChild(0).gameObject)); }
            }
            else if (hasHeadband1 && headband1Health > 0)
            {
                headband1Health -= collision.gameObject.GetComponent<BulletController>().damage;
                if (headband1Health <= 0) { StartCoroutine(_FallObject(1,transform.GetChild(1).gameObject)); }
            }
            else if (hasHeadband2 && headband2Health > 0)
            {
                headband2Health -= collision.gameObject.GetComponent<BulletController>().damage;
                if (headband2Health <= 0) { StartCoroutine(_FallObject(2,transform.GetChild(2).gameObject)); }
            }
            else
            {
                healthPoints -= collision.gameObject.GetComponent<BulletController>().damage;
            }

            Destroy(collision.gameObject);

            if(healthPoints <= 0)
                Destroy(gameObject);
        }
    }

    private IEnumerator _FallObject(int item, GameObject obj)
    {
        yield return new WaitForSeconds(.1f + Random.Range(.1f,.4f));

        if (item == 0 && hasShield)
        {
            hasShield = false;
            yield return null;
        }
        else if (item == 0 && !hasShield) yield break;
        
        if (item == 1 && hasHeadband1)
        {
            hasHeadband1 = false;
            yield return null;
        }
        else if (item == 1 && !hasHeadband1) yield break;

        if (item == 2 && hasHeadband2)
        {
            hasHeadband2 = false;
            yield return null;
        }
        else if (item == 2 && !hasHeadband2) yield break;


        Vector2 _initPos = obj.GetComponent<RectTransform>().anchoredPosition;

        float _elapsed = 0;
        while(_elapsed < 1)
        {
            _elapsed += Time.deltaTime/4.0f;

            obj.GetComponent<Image>().color = Color.Lerp(obj.GetComponent<Image>().color, new Color(obj.GetComponent<Image>().color.r, obj.GetComponent<Image>().color.g, obj.GetComponent<Image>().color.b, 0), _elapsed);
            obj.GetComponent<RectTransform>().anchoredPosition += ((_initPos - new Vector2(0, 200)) - _initPos).normalized * 25.0f * _elapsed;
            yield return null;
        }

    }
    public void OnTriggerExit2D(Collider2D collision)
    {

    }

}
