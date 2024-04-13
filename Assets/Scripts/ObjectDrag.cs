using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public GameObject objectGame;
    public ObjectPlacement objectPlacement;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        objectPlacement = collision.GetComponent<ObjectPlacement>();
    }

    public void TryToPlaceObject()
    {
        if(objectPlacement)
        {
            objectPlacement.TryToPlaceObject(objectGame);
        }
    }
}
