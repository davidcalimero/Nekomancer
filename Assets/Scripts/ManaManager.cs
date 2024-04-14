using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public Vector2 randomSpawnDelaySeconds;
    public GameObject spawnPlace;
    public GameObject summoner;
    public GameObject manaObject;
    public Text manaAmountText;
    public int manaAmount = 0;
    public RectTransform ui;

    public static ManaManager instance;


    void Awake()
    {
        instance = this;
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        SpawnEnergyObject(summoner.transform.position);
        yield return new WaitForSeconds(1);
        SpawnEnergyObject(summoner.transform.position);
    }

    void Update()
    {
        manaAmountText.text = manaAmount.ToString();
    }

    public void SpawnEnergyObject(Vector3 position)
    {
        GameObject obj = Instantiate(manaObject, spawnPlace.transform);
        obj.transform.position = position;
        obj.GetComponent<Mana>().target = ui;

    }
}
