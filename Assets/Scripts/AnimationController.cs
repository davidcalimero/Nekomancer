using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public List<AnimationSprites> anims;
    public float framePerSecond = 12;


    public void Start()
    {
        PlayAnimation(anims[0].name);
    }

    public void PlayAnimation(string name)
    {
        StopAllCoroutines();
        StartCoroutine(_PlayAnimation(name));
    }

    private IEnumerator _PlayAnimation(string name)
    {
        float _timeToChangeFrame = 1.0f / framePerSecond;

        AnimationSprites sprites = anims.Find(x => x.name == name);

        int _frame = 0;
        float _previousChange = 0;
        GetComponent<Image>().sprite = sprites.sprites[_frame];

        while (true)
        {
            float _elapsed = 0;
            while (_elapsed < 1)
            {
                _elapsed += Time.deltaTime;

                if (Mathf.FloorToInt(_elapsed / _timeToChangeFrame) != _previousChange)
                {
                    _previousChange = Mathf.FloorToInt(_elapsed / _timeToChangeFrame);
                    _frame = (_frame + 1) > sprites.sprites.Count - 1 ? 0 : _frame + 1;
                    GetComponent<Image>().sprite = sprites.sprites[_frame];
                }

                yield return null;
            }
            yield return null;
        }
    }
}

[Serializable]
public class AnimationSprites
{
    public string name;
    public List<Sprite> sprites;
}