using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;

    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(direction  * _speed *  Time.deltaTime);
        if(transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }else if(transform.position.y <= -3.8)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11.3f || transform.position.x <= -11.3f)
        {
            //bug here that if player stays at the edge the player will oscillate from one side to the other
            //will have to fix this at some point 
            transform.position = new Vector3(-1*transform.position.x, transform.position.y, 0);
        }
    }
}
