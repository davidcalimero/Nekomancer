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
        if (dragInstance)
        {
            dragInstance.transform.position = Input.mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(HasEnoughMana())
        {
            dragInstance = Instantiate(objectDrag, transform);
            dragInstance.GetComponent<ObjectDrag>().objectGame = objectGame;
            dragInstance.transform.position = Input.mousePosition;
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (dragInstance)
        {
            if (dragInstance.GetComponent<ObjectDrag>().TryToPlaceObject())
            {
                ManaManager.instance.IncrementMana(-manaCost);
                GameFlow.instance.TriggerSummonerAttack();
            }
            Destroy(dragInstance);
            dragInstance = null;
        }
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
        return ManaManager.instance.GetCurrentMana() >= manaCost;
    }

}
