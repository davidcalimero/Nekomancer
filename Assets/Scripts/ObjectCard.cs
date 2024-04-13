using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;

    private GameObject dragInstance;

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        dragInstance.transform.position = Input.mousePosition;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        dragInstance = Instantiate(objectDrag, transform);
        dragInstance.GetComponent<ObjectDrag>().objectGame = objectGame;
        dragInstance.transform.position = Input.mousePosition;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        dragInstance.GetComponent<ObjectDrag>().TryToPlaceObject();
        Destroy(dragInstance);
        dragInstance = null;
    }
}
