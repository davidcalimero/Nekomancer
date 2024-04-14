using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public List<GameObject> startGameObjs;
    public List<GameObject> hideGameObjs;

    public GameObject instructions;

    public void Start()
    {
        if(PlayerPrefs.GetInt("Exit", 1) == 0)
        {
            foreach (GameObject obj in startGameObjs) { obj.SetActive(true); }
            foreach (GameObject obj in hideGameObjs) { obj.SetActive(false); }
            PlayerPrefs.SetInt("Exit", 0);
            PlayerPrefs.Save();
        }
    }

    public void StartGameButton()
    {
        foreach(GameObject obj in startGameObjs) { obj.SetActive(true); }
        foreach (GameObject obj in hideGameObjs) { obj.SetActive(false); }
        PlayerPrefs.SetInt("Exit", 0);
        PlayerPrefs.Save();

        instructions.SetActive(true);
    }

    public void HomeButton()
    {
        PlayerPrefs.SetInt("Exit", 1);
        PlayerPrefs.Save();

        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
