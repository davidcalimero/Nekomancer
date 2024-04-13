using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    public bool isFull;
    public Image containerImage;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isFull)
        {
            containerImage.enabled = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        containerImage.enabled = false;
    }

    public void TryToPlaceObject(GameObject objectGame)
    {
        if(!isFull)
        {
            isFull = true;
            Instantiate(objectGame, transform).transform.position = transform.position;
        }

    }
}
