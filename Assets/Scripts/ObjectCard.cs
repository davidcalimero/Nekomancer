using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;
    public Canvas canvas;

    private GameObject objectDragInstance;

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        objectDragInstance.transform.position = Input.mousePosition;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        objectDragInstance = Instantiate(objectDrag, canvas.transform);
        objectDragInstance.transform.position = Input.mousePosition;
        objectDragInstance.GetComponent<ObjectDrag>().objectCard = this;
        GameManager.instance.draggingObject = objectDragInstance;

    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        GameManager.instance.PlaceObject();
        GameManager.instance.draggingObject = null;
        Destroy(objectDragInstance);
    }
}
