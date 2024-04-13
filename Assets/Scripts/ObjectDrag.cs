using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public GameObject objectGame;
    public ObjectPlacement objectPlacement;

    void OnTriggerEnter2D(Collider2D collision)
    {
        objectPlacement = collision.GetComponent<ObjectPlacement>();
    }

    public bool TryToPlaceObject()
    {
        return objectPlacement && objectPlacement.TryToPlaceObject(objectGame);
    }
}
