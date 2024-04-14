using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform healthBarImage;
    private float maxHealth;
    private float lastFrameHealth;
    private Coroutine scaleCoroutine;

    void Start()
    {
        maxHealth = GameFlow.instance.life;
        lastFrameHealth = maxHealth;
    }

    void Update()
    {
        if (lastFrameHealth != GameFlow.instance.life)
        {
            if (scaleCoroutine != null)
            {
                StopCoroutine(scaleCoroutine);
            }
            scaleCoroutine = StartCoroutine(AnimateScale(GameFlow.instance.life / maxHealth));
            lastFrameHealth = GameFlow.instance.life;
        }
    }

    IEnumerator AnimateScale(float targetScaleX)
    {
        float duration = 0.2f;
        float currentTime = 0f;
        Vector3 startScale = healthBarImage.localScale;
        Vector3 endScale = new Vector3(targetScaleX, startScale.y, startScale.z);

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / duration;
            t = t * t * (3f - 2f * t);
            healthBarImage.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        healthBarImage.localScale = endScale;
    }
}