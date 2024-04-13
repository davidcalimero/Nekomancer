using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float damage = 5.0f;
    public float speed = 1.0f;


    void Update()
    {
        if (GameFlow.instance.gameFinished)
        {
            return;
        }

        transform.GetComponent<RectTransform>().anchoredPosition += Vector2.up * speed * 100 * Time.deltaTime;
        if (transform.GetComponent<RectTransform>().anchoredPosition.y > 1520) DestroyImmediate(gameObject);
    }
}
