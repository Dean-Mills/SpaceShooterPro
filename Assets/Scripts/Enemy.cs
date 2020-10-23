using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    [SerializeField]
    private Player _player1;
    [SerializeField]
    private Player _player2;
    [SerializeField]
    private GameObject _laser;
    private float _fireRate = 3f;
    private float _canFire = -1f;
    private Animator _animator;
    private AudioSource _audioSource;
    private GameManager _gameManager;
    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        
        if (_animator == null)
        {
            Debug.LogError("[Enemy] The animator object is null");
        }
        if (_gameManager == null)
        {
            Debug.LogError("[Enemy] The gameManager object is null");
        }
        if (_audioSource == null)
        {
            Debug.LogError("[Enemy] The audiosource object is null");
        }

        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        if (_gameManager._isCoop)
        {
            _player2 = GameObject.Find("Player2").GetComponent<Player>();
            if (_player2 == null)
            {
                Debug.LogError("[Enemy] Player2 object is null");
            }
        }
        if (_player1 == null)
        {
            Debug.LogError("[Enemy] Player1 object is null");
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
            if (_player1 != null)
            {
                _player1.AddScore(10);
            }
            _audioSource.Play();
        }
        if(other.tag == "Player")
        {
            _animator.SetTrigger("OnEnemyDeath");
            Destroy(gameObject.GetComponent<BoxCollider2D>());
            _speed = 0;
            Destroy(this.gameObject, 2.38f);
            if(_player1 != null)
            {
                _player1.Damage();
            }
            _audioSource.Play();
        }
    }
}
