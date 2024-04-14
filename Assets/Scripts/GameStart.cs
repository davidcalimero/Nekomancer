using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public List<GameObject> startGameObjs;
    public List<GameObject> hideGameObjs;

    public void StartGameButton()
    {
        foreach(GameObject obj in startGameObjs) { obj.SetActive(true); }
        foreach (GameObject obj in hideGameObjs) { obj.SetActive(false); }
    }
}
