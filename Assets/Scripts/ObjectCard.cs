using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectCard : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public GameObject objectDrag;
    public GameObject objectGame;
    public Text textCost;
    public int manaCost;

    private GameObject dragInstance;

    void Start()
    {
        textCost.text = manaCost.ToString();
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        dragInstance = Instantiate(objectDrag, transform);
        dragInstance.GetComponent<ObjectDrag>().objectGame = objectGame;
        dragInstance.transform.position = Input.mousePosition;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(HasEnoughMana() && dragInstance.GetComponent<ObjectDrag>().TryToPlaceObject())
        {
            ManaManager.instance.manaAmount -= manaCost;
        }
        Destroy(dragInstance);
        dragInstance = null;
    }

    void Update()
    {
        if(HasEnoughMana())
        {
            GetComponent<Image>().color = Color.white;
        } else
        {
            GetComponent<Image>().color = Color.grey;
        }
        
    }

    private bool HasEnoughMana()
    {
        return ManaManager.instance.manaAmount >= manaCost;
    }

}
