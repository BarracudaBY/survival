using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyToSpawn;
    public float TimeToSpawn;
    public Transform MinSpawn, MaxSpawn;


    private float _spawnCounter;
    private float _deSpawnDistance;
    private Transform _target;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    public int CheckPerFrame;
    private int _enemyToCheck;

    public List<WaveInfo> Waves;
    private int _currentWave;
    private float _waveCounter;

    private void Start()
    {
        //_spawnCounter = TimeToSpawn;

        _target = PlayerHealthController.Instance.transform;

        _deSpawnDistance = Vector3.Distance(transform.position , MaxSpawn.position) + 4f;

        _currentWave = -1;
        GoToNextWave();
    }

    private void Update()
    {
        /*_spawnCounter -= Time.deltaTime;

        if(_spawnCounter <= 0)
        {
            _spawnCounter = TimeToSpawn;

            //Instantiate(EnemyToSpawn, transform.position, transform.rotation);
            GameObject newEnemy = Instantiate(EnemyToSpawn, SelectSpawnPoint(), transform.rotation);

            _spawnedEnemies.Add(newEnemy);
        }*/

        if (PlayerHealthController.Instance.gameObject.activeSelf)
        {
            if(_currentWave < Waves.Count)
            {
                _waveCounter -= Time.deltaTime;

                if(_waveCounter <= 0)
                {
                    GoToNextWave();
                }

                _spawnCounter -= Time.deltaTime;
                if(_spawnCounter <= 0)
                {
                    _spawnCounter = Waves[_currentWave].TimeBetweenSpawn;

                    GameObject newEnemy = Instantiate(Waves[_currentWave].EnemyToSpawn, SelectSpawnPoint(), Quaternion.identity);

                    _spawnedEnemies.Add(newEnemy);
                }
            }
        }

        transform.position = _target.position;

        int checkTarget = CheckPerFrame + _enemyToCheck;

        while (_enemyToCheck < checkTarget)
        {
            if(_enemyToCheck < _spawnedEnemies.Count)
            {
                if (_spawnedEnemies[_enemyToCheck] != null)
                {
                    if(Vector3.Distance(transform.position , _spawnedEnemies[_enemyToCheck].transform.position) > _deSpawnDistance)
                    {
                        Destroy(_spawnedEnemies[_enemyToCheck]);

                        _spawnedEnemies.RemoveAt(_enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        _enemyToCheck++;
                    }
                }
                else
                {
                    _spawnedEnemies.RemoveAt(_enemyToCheck);
                    checkTarget --;
                }
            }
            else
            {
                _enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f;

        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(MinSpawn.position.y, MaxSpawn.position.y);

            if(Random.Range(0f , 1f) > 0.5f)
            {
                spawnPoint.x = MaxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = MinSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(MinSpawn.position.x, MaxSpawn.position.x);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.y = MaxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = MinSpawn.position.y;
            }
        }



        return spawnPoint;
    }

    public void GoToNextWave()
    {
        _currentWave++;

        if(_currentWave >= Waves.Count)
        {
            _currentWave = Waves.Count - 1;
        }

        _waveCounter = Waves[_currentWave].WaveLength;
        _spawnCounter = Waves[_currentWave].TimeBetweenSpawn;
    }

}

[System.Serializable]
public class WaveInfo
{
    public GameObject EnemyToSpawn;

    public float WaveLength = 10f;
    public float TimeBetweenSpawn = 1f;

}
