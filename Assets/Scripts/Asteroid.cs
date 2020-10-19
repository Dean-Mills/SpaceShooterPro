using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _rotateSpeed;
    [SerializeField]
    private GameObject _explosionEffect;
    // Update is called once per frame
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
        }
        
    }
}
