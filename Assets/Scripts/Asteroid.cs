using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private GameObject _explosionEffect;
    private SpawnManager _spawnManager;
    private void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("Could not find the spawn manager");
        }
    }
    void Update()
    {
        gameObject.transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            var pos = gameObject.transform.position;
            Destroy(gameObject, 0.2f);
            Destroy(other.gameObject);
            Instantiate(_explosionEffect, pos, Quaternion.identity);
            _spawnManager.StartSpawning();
        }
        
    }
}
