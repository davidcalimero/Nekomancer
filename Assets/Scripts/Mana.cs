using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    public int manaAmount;
    public RectTransform target;

    void Start()
    {
        StartMovement();
    }

    public Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }

    IEnumerator MoveObjectAlongCurve(GameObject obj, Vector3 start, Vector3 controlPoint, Vector3 end, float duration)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            Vector3 position = CalculateBezierPoint(t, start, controlPoint, end);
            obj.transform.position = position;
            elapsed += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = end;

        ManaManager.instance.IncrementMana(manaAmount);
        Destroy(gameObject);
    }

    void StartMovement()
    {
        Vector3 startPoint = gameObject.transform.position;
        Vector3 endPoint = target.transform.position;
        int randomSign = Random.value < 0.5 ? -1 : 1;
        Vector3 controlPoint = (startPoint + endPoint) / 2 + new Vector3(200 * randomSign, 200, 0);

        StartCoroutine(MoveObjectAlongCurve(gameObject, startPoint, controlPoint, endPoint, 0.3f));
    }
}
