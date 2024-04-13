using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Mana : MonoBehaviour, IPointerDownHandler
{
    public int manaAmount;

    public void OnPointerDown(PointerEventData eventData)
    {
        ManaManager.instance.manaAmount += manaAmount;
        Destroy(gameObject);
    }
}
