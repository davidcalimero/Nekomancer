using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemySuperPrefab;
    public GameObject summoner;

    public List<int> occupationNum = new List<int>();

    public bool generateEnergy = false;

    private List<GameObject> _enemyLanes; 

    public void Start()
    {
        _enemyLanes = GameObject.FindGameObjectsWithTag("EnemyLane").ToList();
        
        foreach(GameObject enemyLane in _enemyLanes) { occupationNum.Add(0); }
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GenerateNewRandomEnemy();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //GenerateNewRandomEnemySuper(10000);
        }
    }

    public void GenerateNewRandomEnemy()
    {
        int _randomIndex = (int) Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemyPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.transform.SetSiblingIndex(2);

        bool hasShield = Random.value >= 0.5;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShield = hasShield;
        if(!hasShield)
        {
            _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShieldDamage = Random.value >= 0.5;
        }

        bool hasHelmet = Random.value >= 0.5;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelment = hasHelmet;
        if(!hasHelmet)
        {
            _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelmentDamange = Random.value >= 0.5;
        }
        
               

        occupationNum[_randomIndex]++;
    }

    public float GenerateNewRandomEnemy(float challengeLevel)
    {
        int _randomIndex = (int)Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemyPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().summoner = summoner;
        _newEnemy.transform.SetSiblingIndex(2);

        _newEnemy.GetComponentInChildren<EnemyBehaviour>().healthPoints = challengeLevel;

        bool hasShield = Random.value >= 0.5;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShield = hasShield;
        if (!hasShield)
        {
            _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShieldDamage = Random.value >= 0.5;
        }

        bool hasHelmet = Random.value >= 0.5;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelment = hasHelmet;
        if (!hasHelmet)
        {
            _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelmentDamange = Random.value >= 0.5;
        }


        occupationNum[_randomIndex]++;

        float _health = ((_newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShield) ? 5 : 0) + ((_newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelment) ? 5 : 0) + challengeLevel;

        _newEnemy.GetComponentInChildren<EnemyBehaviour>().healthMaxPoints = _health;

        return _health;
    }

    public float GenerateNewRandomEnemySuper(float challengeLevel)
    {
        int _randomIndex = (int)Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemySuperPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().summoner = summoner;
        _newEnemy.transform.SetSiblingIndex(2);

        _newEnemy.GetComponentInChildren<EnemyBehaviour>().healthPoints = challengeLevel;

        bool hasShield = Random.value >= 0.5;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShield = hasShield;
        if (!hasShield)
        {
            _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShieldDamage = Random.value >= 0.5;
        }

        bool hasHelmet = Random.value >= 0.5;
        _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelment = hasHelmet;
        if (!hasHelmet)
        {
            _newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelmentDamange = Random.value >= 0.5;
        }


        occupationNum[_randomIndex]++;

        float _health = ((_newEnemy.GetComponentInChildren<EnemyBehaviour>().hasShield) ? 10 : 0) + ((_newEnemy.GetComponentInChildren<EnemyBehaviour>().hasHelment) ? 10 : 0) + challengeLevel;

        _newEnemy.GetComponentInChildren<EnemyBehaviour>().healthMaxPoints = _health;

        return _health;
    }

    public void UpdateDeletedEnemy(int index, Vector3 position)
    {
        occupationNum[index]--;

        if (generateEnergy)
        {
            generateEnergy = false;
            FindObjectOfType<ManaManager>().SpawnEnergyObject(position);
        }
    }

    public int GetIfAloneLane(int index)
    {
        int _totalEnemies = occupationNum.Sum();

        if(_totalEnemies > 20 && occupationNum.Any(x => x == 0))
        {
            int _nextIndex = occupationNum.FindIndex(x => x == 0);
            return _nextIndex;
        }

        return index;
    }
}
