using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    public float healthPoints = 20;
    public float healthMaxPoints = 20;
    public float speed = 1.0f;

    public float damage = 5.0f;
    public float rateDamage = 3.0f;

    public int indexLane;

    public bool hasShield;
    public float shieldHealth = 3;
    public bool hasShieldDamage;
    public float shieldDamageHealth = 2;
    public bool hasHelment;
    public float heltmetHealth = 3;
    public bool hasHelmentDamange;
    public float helmentDamangeHealth = 2;
    
    public RectTransform initPos;
    public RectTransform endPos;
    public GameObject summoner;

    public Animator animator;

    private bool _isAttacking = false;
    private float _variation = 1.0f;
    private float _variationNextEnemy = 15.0f;

    private float _lastDamage = 0;

    private GameObject _evocationAttacking = null;
    private float _attackVariation = 0;
    private bool facingSummoner = false;
    private bool reachedSummoner = false;

    public IEnumerator Start()
    {
        transform.GetComponent<RectTransform>().position = initPos.position;

        _variation = Random.Range(0.7f, 0.9f);
        _variationNextEnemy = Random.Range(10, 45);

        _lastDamage = Time.time;
        _attackVariation = Random.Range(0.2f, 0.9f);

        yield return new WaitForSeconds(.2f);

        UpdateVisuals();
    }

    public void Update()
    {
        if(GameFlow.instance.gameFinished)
        {
            return;
        }

        if (_isAttacking && _evocationAttacking)
        {
            if(Time.time - _lastDamage > rateDamage + _attackVariation)
            {
                animator.SetTrigger("Attack");
            }
        }
        else if (!facingSummoner && Vector2.Distance(transform.GetComponent<RectTransform>().position, endPos.position) > 0.1f)
        {
            if(transform.GetSiblingIndex() < transform.parent.childCount - 1)
            {
                if(Vector2.Distance(transform.GetComponent<RectTransform>().position, transform.parent.GetChild(transform.GetSiblingIndex()+1).GetComponent<RectTransform>().position) < _variationNextEnemy)
                {
                    return;
                }
            }

            transform.GetComponent<RectTransform>().position += (endPos.position - transform.GetComponent<RectTransform>().position).normalized * 50 * _variation * speed * Time.deltaTime;
            facingSummoner = Vector2.Distance(transform.GetComponent<RectTransform>().position, endPos.position) <= 0.1f;
        }
        else if(facingSummoner && !reachedSummoner && Vector2.Distance(transform.GetComponent<RectTransform>().position, summoner.GetComponent<RectTransform>().position) > 0.1f)
        {
            transform.GetComponent<RectTransform>().position += (summoner.GetComponent<RectTransform>().position - transform.GetComponent<RectTransform>().position).normalized * 50 * _variation * speed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (_evocationAttacking != null)
        {
            _evocationAttacking.GetComponent<EvocationController>().DamageFromEnemy(damage);
            if (_evocationAttacking.GetComponent<EvocationController>().healthPoints <= 0)
            {
                _isAttacking = false;
                _evocationAttacking.transform.parent.GetComponent<ObjectPlacement>().isFull = false;
                _evocationAttacking = null;
            }
        }

        if(reachedSummoner)
        {
            FindObjectOfType<EnemyWaveController>().UpdateDeletedEnemy(indexLane);
            GameFlow.instance.LoseLife(damage);
            Destroy(gameObject);
        }

        _lastDamage = Time.time;
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
            if (hasShield)
            {
                shieldHealth -= collision.gameObject.GetComponent<BulletController>().damage;
                if(shieldHealth <= 0)
                {
                    hasShield = false;
                    hasShieldDamage = true;
                    UpdateVisuals();
                }
            }
            else if(hasShieldDamage)
            {
                shieldDamageHealth -= collision.gameObject.GetComponent<BulletController>().damage;
                if (shieldDamageHealth <= 0) {
                    hasShieldDamage = false;
                    StartCoroutine(_FallObject(3, transform.GetChild(3).gameObject));
                }
            }
            else if (hasHelment)
            {
                heltmetHealth -= collision.gameObject.GetComponent<BulletController>().damage;
                if (heltmetHealth <= 0)
                {
                    hasHelment = false;
                    hasHelmentDamange = true;
                    UpdateVisuals();
                }
            }
            else if (hasHelmentDamange)
            {
                helmentDamangeHealth -= collision.gameObject.GetComponent<BulletController>().damage;
                if (helmentDamangeHealth <= 0) {
                    hasHelmentDamange = false;
                    StartCoroutine(_FallObject(1,transform.GetChild(1).gameObject));
                }
            }
            else
            {
                healthPoints -= collision.gameObject.GetComponent<BulletController>().damage;
            }

            Destroy(collision.gameObject);

            if(healthPoints <= 0)
                Destroy(gameObject);
        }

        if(collision.tag.Equals("Player") && !reachedSummoner)
        {
            reachedSummoner = true;
            animator.SetTrigger("Attack");
            StartCoroutine(_FallObject(0, transform.GetChild(0).gameObject));
            StartCoroutine(_FallObject(1, transform.GetChild(1).gameObject));
            StartCoroutine(_FallObject(2, transform.GetChild(2).gameObject));
            StartCoroutine(_FallObject(3, transform.GetChild(3).gameObject));
        }
    }

    private void UpdateVisuals()
    {
        transform.GetChild(0).gameObject.SetActive(hasHelment);
        transform.GetChild(1).gameObject.SetActive(hasHelmentDamange);
        transform.GetChild(2).gameObject.SetActive(hasShield);
        transform.GetChild(3).gameObject.SetActive(hasShieldDamage);
    }

    private IEnumerator _FallObject(int item, GameObject obj)
    {
        yield return new WaitForSeconds(.1f + Random.Range(.1f,.4f));

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
