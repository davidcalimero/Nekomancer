using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float speed = 1.0f;

    public int indexLane;
    public RectTransform initPos;
    public RectTransform endPos;

    private bool _isAttacking = false;
    private float _variation = 1.0f;
    private float _variationNextEnemy = 15.0f;

    public void Start()
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos.anchoredPosition;

        _variation = Random.Range(0.7f, 0.9f);
        _variationNextEnemy = Random.Range(10, 45);
    }

    public void Update()
    {
        if (_isAttacking)
        {

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
        if (collision.tag.Equals("something"))
        {
            _isAttacking = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("something"))
        {
            _isAttacking = false;
        }
    }

}
