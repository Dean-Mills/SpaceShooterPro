using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _laser;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    private Animator _animator;
    private AudioSource _audioSource;
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _animator = gameObject.GetComponent<Animator>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        if (_player == null)
        {
            Debug.LogError("[Enemy] The player object is null");
        }
        if (_animator == null)
        {
            Debug.LogError("[Enemy] The animator object is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("[Enemy] The audiosource object is null");
        }
    }
    void Update()
    {
        CalculateMovement();
        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7f);
            _canFire = Time.time + _fireRate;
            var enemyLaser =  Instantiate(_laser, this.transform.position, Quaternion.identity);
            var children = enemyLaser.GetComponentsInChildren<Laser>();
            foreach(var child in children)
            {
                child.SetDirectionDown();
            }
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            var randomX = Random.Range(-10f, 10f);
            var randomY = Random.Range(5.5f, 6f);
            transform.position = new Vector3(randomX, randomY, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Laser")
        {
            Destroy(other.gameObject);
            _speed = 0;
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            Destroy(this.gameObject, 2.38f);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            _audioSource.Play();
        }
        if(other.tag == "Player")
        {
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            _speed = 0;
            Destroy(this.gameObject, 2.38f);
            if(_player != null)
            {
                _player.Damage();
            }
            _audioSource.Play();
        }
    }
}
