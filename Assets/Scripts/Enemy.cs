using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Player _player;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("[Enemy] The player object is null");
        }
    }
    void Update()
    {   
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if(transform.position.y < -4.5f)
        {
            var randomX = Random.Range(-10f, 10f);
            var randomY = Random.Range(5.5f, 6f);
            transform.position = new Vector3(randomX, randomY, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if(other.tag == "Laser")
        {
            Destroy(this.gameObject);
            //check if player is dead
            if (_player != null)
            {
                _player.AddScore(10);
            }
            Destroy(other.gameObject);
        }
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            if(_player != null)
            {
                _player.Damage();
            }
        }
    }
}
