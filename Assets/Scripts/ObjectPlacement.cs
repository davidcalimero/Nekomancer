using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    public bool isFull;
    public Image containerImage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isFull)
        {
            containerImage.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        containerImage.enabled = false;
    }

    public bool TryToPlaceObject(GameObject objectGame)
    {
        if(!isFull)
        {
            isFull = true;
            GameObject obj = Instantiate(objectGame, transform.parent.GetChild(0));
            obj.transform.position = transform.position;
            obj.GetComponent<EvocationController>().placement = this;

            for(int i = obj.transform.GetSiblingIndex() - 1; i > 1; i--)
            {
                if (obj.transform.GetComponent<RectTransform>().anchoredPosition.y > transform.parent.GetChild(0).GetChild(i).GetComponent<RectTransform>().anchoredPosition.y)
                    obj.transform.SetSiblingIndex(i);
            }

            return true;
        }
        return false;
    }
}
