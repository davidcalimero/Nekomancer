using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    public Image containerImage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.instance.draggingObject && !isFull)
        {
            GameManager.instance.currentContainer = gameObject;
            containerImage.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        GameManager.instance.currentContainer = null;
        containerImage.enabled = false;
    }
}
