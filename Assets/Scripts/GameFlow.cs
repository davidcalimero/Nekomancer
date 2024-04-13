using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public float life;
    public GameObject losePopup;
    public bool gameFinished = false;

    public static GameFlow instance;

    void Awake()
    {
        instance = this;
    }

    public void LoseLife(float amount)
    {
        life -= amount;
        if(life <= 0)
        {
            losePopup.SetActive(true);
            gameFinished = true;
        }
    }

    public void Retry()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
