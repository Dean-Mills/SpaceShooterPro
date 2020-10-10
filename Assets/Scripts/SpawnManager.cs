using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float SpawnTime;
    public GameObject Enemy;
    private IEnumerator coroutine;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = SpawnEnemy();
        StartCoroutine(coroutine);
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

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
