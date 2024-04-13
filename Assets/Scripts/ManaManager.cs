using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    public Vector2 randomSpawnDelaySeconds;
    public GameObject[] spawnPlaces;
    public GameObject manaObject;
    public int manaAmount = 0;

    private float nextSpawnTime;

    public static ManaManager instance;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        SetNextSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnergyObject();
            SetNextSpawnTime();
        }
    }

    void SetNextSpawnTime()
    {
        nextSpawnTime = Time.time + Random.Range(randomSpawnDelaySeconds.x, randomSpawnDelaySeconds.y);
    }

    void SpawnEnergyObject()
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
}
