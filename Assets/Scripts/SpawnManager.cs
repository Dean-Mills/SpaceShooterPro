using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float SpawnTime;
    public GameObject Enemy;
    public GameObject [] PowerUps;
    //public GameObject SpeedBoostPowerUp;
    private IEnumerator enemyCoroutine;
    private IEnumerator powerUpCoroutine;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    
    public void StartSpawning()
    {
        enemyCoroutine = SpawnEnemy();
        powerUpCoroutine = SpawnPowerup();
        StartCoroutine(enemyCoroutine);
        StartCoroutine(powerUpCoroutine);
        //You can also do it like this but then you don't really get the ability to start and stop it externally
        //StartCoroutine(SpawnEnemy());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnEnemy()
    {
        while (!_stopSpawning)
        { 
            var enemyPos = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            var enemy = Instantiate(Enemy, enemyPos, Quaternion.identity);
            enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(SpawnTime); //Just so an enemy spawns immediately.... MIght change this later
        }
    }
    private IEnumerator SpawnPowerup()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(Random.Range(3f, 7f));
            var powerUpPos = new Vector3(Random.Range(-8f, 8f), 7f, 0);
            int rand = Random.Range(0, 3);
            Instantiate(PowerUps[rand], powerUpPos, Quaternion.identity);
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
