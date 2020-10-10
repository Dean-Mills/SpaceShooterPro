using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
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
            Destroy(other.gameObject);
        }
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            var player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            
        }
    }
}
