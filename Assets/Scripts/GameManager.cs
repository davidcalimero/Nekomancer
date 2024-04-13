using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject draggingObject;
    public GameObject currentContainer;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void PlaceObject()
    {
        if(draggingObject && currentContainer)
        {
            Instantiate(draggingObject.GetComponent<ObjectDrag>().objectCard.objectGame, currentContainer.transform).transform.position = currentContainer.transform.position;
            currentContainer.GetComponent<ObjectContainer>().isFull = true;
        }
    }
}
