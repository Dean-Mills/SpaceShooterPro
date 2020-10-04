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
        CalculateMovement();
    }

    void CalculateMovement()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, vertical, 0).normalized;
        transform.Translate(direction * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0f), 0);
        if (transform.position.x >= 11.3f || transform.position.x <= -11.3f)
        {
            //bug here that if player stays at the edge the player will oscillate from one side to the other
            //will have to fix this at some point 
            transform.position = new Vector3(-1 * transform.position.x, transform.position.y, 0);
        }
    }
}
