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
            Instantiate(objectGame, transform).transform.position = transform.position;
            return true;
        }
        return false;
    }
}
