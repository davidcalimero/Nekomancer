using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDrag : MonoBehaviour
{
    public GameObject objectGame;
    public ObjectPlacement objectPlacement;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Placement"))
            objectPlacement = collision.GetComponent<ObjectPlacement>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Placement"))
            objectPlacement = null;
    }

    public bool TryToPlaceObject()
    {
        return objectPlacement && objectPlacement.TryToPlaceObject(objectGame);
    }
}
