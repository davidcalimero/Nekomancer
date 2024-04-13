using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public Vector2 rangeChallenge = new Vector2(-35, 35);

    public float totalhealthPlayer = 0;
    public float energyPlayer = 0;

    public float currentChallengeLevel = 0;

    private float timePerWave = 10;

    private float _lastWave = 0;

    private int countLessZero = 0;

    public void Start()
    {
        _lastWave = Time.time;

        StopAllCoroutines();
        GenerateWave(10);
    }

    public void Update()
    {
        if (Time.time - _lastWave > timePerWave)
        {
            print("new wave");
            StopAllCoroutines();

            float _randomValue = Random.Range(rangeChallenge.x, rangeChallenge.y);
            if (_randomValue < 0 && countLessZero > 3) { _randomValue = Random.Range(0, rangeChallenge.y); countLessZero = 0; }

            if (_randomValue < 0) countLessZero++;

            GenerateWave(currentChallengeLevel + _randomValue);

            _lastWave = Time.time;
        }
    }

    public void GenerateWave(float challengeLevel)
    {
        StartCoroutine(_GenerateWave(challengeLevel));
    }

    private IEnumerator _GenerateWave(float challengeLevel)
    {
        energyPlayer = FindObjectOfType<ManaManager>().manaAmount;
        totalhealthPlayer = GameObject.FindGameObjectsWithTag("Evocation").ToList().Sum(x => x.GetComponent<EvocationController>().healthPoints);

        float challengeStrength = challengeLevel - (energyPlayer + totalhealthPlayer);
        print(challengeStrength);
        float totalStrength = challengeLevel;

        while (totalStrength > 0)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));

            if (challengeStrength > 0)
            {
                FindObjectOfType<ManaManager>().SpawnEnergyObject();
            }

            int _numEnemies = Random.Range(1, 7);

            for (int i = 0; i < _numEnemies; i++)
            {
                totalStrength -= FindObjectOfType<EnemyWaveController>().GenerateNewRandomEnemy(challengeLevel / _numEnemies);
                print("generated " + (challengeLevel / _numEnemies));
                yield return new WaitForSeconds(Random.Range(0, 4));
            }
            yield return null;
        }
        print("end");
        currentChallengeLevel = challengeLevel;
    }
}
