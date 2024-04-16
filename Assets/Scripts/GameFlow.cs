using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFlow : MonoBehaviour
{
    public float life;
    public GameObject losePopup;
    public bool gameFinished = false;
    public GameObject summoner;

    public static GameFlow instance;

    private bool retry;

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
            summoner.GetComponent<SummonerController>().Die();
            life = 0;
        }
    }

    public void Retry()
    {
        retry = true;
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void TriggerSummonerAttack()
    {
        summoner.GetComponent<SummonerController>().Attack();
    }

    private void OnDestroy()
    {
        if (retry) return;
        PlayerPrefs.SetInt("Exit", 1);
        PlayerPrefs.Save();
    }

    void OnApplicationPause()
    {
        if (retry) return;
        PlayerPrefs.SetInt("Exit", 1);
        PlayerPrefs.Save();

        //string currentSceneName = SceneManager.GetActiveScene().name;
        //SceneManager.LoadScene(currentSceneName);
    }
}
