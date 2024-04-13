using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyWaveController : MonoBehaviour
{
    public GameObject enemyPrefab;

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
            GenerateNewRandomEnemy();
        }
    }

    private void GenerateNewRandomEnemy()
    {
        int _randomIndex = (int) Random.Range(0, _enemyLanes.Count);

        GameObject _newEnemy = Instantiate(enemyPrefab, _enemyLanes[_randomIndex].transform);
        _newEnemy.GetComponent<EnemyBehaviour>().initPos = _enemyLanes[_randomIndex].transform.GetChild(0).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().endPos = _enemyLanes[_randomIndex].transform.GetChild(1).GetComponent<RectTransform>();
        _newEnemy.GetComponent<EnemyBehaviour>().indexLane = _randomIndex;
        _newEnemy.transform.SetSiblingIndex(2);

        occupationNum[_randomIndex]++;
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
