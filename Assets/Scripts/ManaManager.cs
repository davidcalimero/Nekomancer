using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Vector2 randomSpawnDelaySeconds;
    public GameObject[] spawnPlaces;
    public GameObject spawnPlace;
    public GameObject manaObject;
    public Text manaAmountText;
    public int manaAmount = 0;

    private float nextSpawnTime;

    public static ManaManager instance;


    void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        SpawnEnergyObjectRandom();
        yield return new WaitForSeconds(3);
        SpawnEnergyObjectRandom();
    }

    // Update is called once per frame
    void Update()
    {
        manaAmountText.text = manaAmount.ToString();
        //if (Time.time >= nextSpawnTime)
        //{
        //    //SpawnEnergyObject();
        //    //SetNextSpawnTime();
        //}
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(randomSpawnDelaySeconds.x, randomSpawnDelaySeconds.y);
    }

    public void SpawnEnergyObjectRandom()
    {
        if (spawnPlaces.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPlaces.Length);
            if (spawnPlaces[randomIndex] != null)
            {
                Instantiate(manaObject, spawnPlaces[randomIndex].transform).transform.position = spawnPlaces[randomIndex].transform.position;
            }
        }
    }

    public void SpawnEnergyObject(Vector3 position)
    {
        GameObject obj = Instantiate(manaObject, spawnPlace.transform);
        obj.transform.position = position;
           
    }
}
