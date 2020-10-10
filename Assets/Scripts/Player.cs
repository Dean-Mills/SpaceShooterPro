using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    public GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {
        CalculateMovement();
        //if I hit the space key spawn a game object
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
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

    void FireLaser()
    {
        
        var hold = transform.position;
        hold.y += 0.8f;
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, hold, Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;
        
        if(_lives <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
