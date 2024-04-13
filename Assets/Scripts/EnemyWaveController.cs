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
    }

    public void GenerateNewRandomEnemy()
    {
        int _randomIndex = (int) Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemyPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponent<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.transform.SetSiblingIndex(2);

        bool hasShield = Random.value >= 0.5;
        _newEnemy.GetComponent<EnemyBehaviour>().hasShield = hasShield;
        if(!hasShield)
        {
            _newEnemy.GetComponent<EnemyBehaviour>().hasShieldDamage = Random.value >= 0.5;
        }

        bool hasHelmet = Random.value >= 0.5;
        _newEnemy.GetComponent<EnemyBehaviour>().hasHelment = hasHelmet;
        if(!hasHelmet)
        {
            _newEnemy.GetComponent<EnemyBehaviour>().hasHelmentDamange = Random.value >= 0.5;
        }
        
               

        occupationNum[_randomIndex]++;
    }

    public float GenerateNewRandomEnemy(float challengeLevel)
    {
        int _randomIndex = (int)Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemyPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponent<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.GetComponent<EnemyBehaviour>().summoner = summoner;
        _newEnemy.transform.SetSiblingIndex(2);

        _newEnemy.GetComponent<EnemyBehaviour>().healthPoints = challengeLevel;

        bool hasShield = Random.value >= 0.5;
        _newEnemy.GetComponent<EnemyBehaviour>().hasShield = hasShield;
        if (!hasShield)
        {
            _newEnemy.GetComponent<EnemyBehaviour>().hasShieldDamage = Random.value >= 0.5;
        }

        bool hasHelmet = Random.value >= 0.5;
        _newEnemy.GetComponent<EnemyBehaviour>().hasHelment = hasHelmet;
        if (!hasHelmet)
        {
            _newEnemy.GetComponent<EnemyBehaviour>().hasHelmentDamange = Random.value >= 0.5;
        }


        occupationNum[_randomIndex]++;

        float _health = ((_newEnemy.GetComponent<EnemyBehaviour>().hasShield) ? 5 : 0) + ((_newEnemy.GetComponent<EnemyBehaviour>().hasHelment) ? 5 : 0) + challengeLevel;

        _newEnemy.GetComponent<EnemyBehaviour>().healthMaxPoints = _health;

        return _health;
    }

    public float GenerateNewRandomEnemySuper(float challengeLevel)
    {
        int _randomIndex = (int)Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemySuperPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponent<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.GetComponent<EnemyBehaviour>().summoner = summoner;
        _newEnemy.transform.SetSiblingIndex(2);

        _newEnemy.GetComponent<EnemyBehaviour>().healthPoints = challengeLevel;

        bool hasShield = Random.value >= 0.5;
        _newEnemy.GetComponent<EnemyBehaviour>().hasShield = hasShield;
        if (!hasShield)
        {
            _newEnemy.GetComponent<EnemyBehaviour>().hasShieldDamage = Random.value >= 0.5;
        }

        bool hasHelmet = Random.value >= 0.5;
        _newEnemy.GetComponent<EnemyBehaviour>().hasHelment = hasHelmet;
        if (!hasHelmet)
        {
            _newEnemy.GetComponent<EnemyBehaviour>().hasHelmentDamange = Random.value >= 0.5;
        }


        occupationNum[_randomIndex]++;

        float _health = ((_newEnemy.GetComponent<EnemyBehaviour>().hasShield) ? 10 : 0) + ((_newEnemy.GetComponent<EnemyBehaviour>().hasHelment) ? 10 : 0) + challengeLevel;

        _newEnemy.GetComponent<EnemyBehaviour>().healthMaxPoints = _health;

        return _health;
    }

    public void UpdateDeletedEnemy(int index)
    {
        occupationNum[index]--;
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
