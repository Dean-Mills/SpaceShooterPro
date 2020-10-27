using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    public GameObject _laserPrefab;
    public GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = 0.0f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField]
    private bool _hasTripleShot = false;
    [SerializeField]
    private bool _hasShield = false;
    [SerializeField]
    private GameObject _shield;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;
    [SerializeField]
    private float _tripleShotActiveTime = 5f;
    private IEnumerator coroutine;
    [SerializeField]
    private int _score = 0;

    private UIManager _UIManager;
    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource _audioSource;

    [SerializeField]
    private bool _isPlayer1;
    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();
        _shield.SetActive(false);
        _rightEngine.SetActive(false);
        _leftEngine.SetActive(false);
        _UIManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("The spawn manager is NULL");
        }
        if (_UIManager == null)
        {
            Debug.LogError("The ui manager is NULL");
        }
        if (_gameManager == null)
        {
            Debug.LogError("The game manager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The audio source is null");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
        WhoAmI();
    }
    void WhoAmI()
    {
        _isPlayer1 = this.gameObject.tag == "Player";
    }
    void Update()
    {
        CalculateMovement();
        //if I hit the space key spawn a game object
        if ((_isPlayer1 ? Input.GetKeyDown(KeyCode.Space) : Input.GetButtonDown("Fire1")) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        var horizontal = _isPlayer1 ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal1");
        var vertical = _isPlayer1 ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical1");
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
        hold.y += 1f;
        _canFire = Time.time + _fireRate;
        if(_hasTripleShot)
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        else
            Instantiate(_laserPrefab, hold, Quaternion.identity);

        _audioSource.Play();
    }

    public void TripleShotCollected()
    {
        coroutine = RemoveTripleShotPowerUp();
        StartCoroutine(coroutine);
        //Again you can just 
        //StartCoroutine(RemovePowerUp());
        _hasTripleShot = true;
    }

    public void SpeedBoostCollected()
    {
        StartCoroutine(RemoveSpeedBoost());
        _speed = 8f;
    }

    public void ShieldCollected()
    {
        _hasShield = true;
        _shield.SetActive(true);
    }

    private IEnumerator RemoveSpeedBoost()
    {
        yield return new WaitForSeconds(_tripleShotActiveTime);
        _speed = 3.5f;
    }
    private IEnumerator RemoveTripleShotPowerUp()
    {
        yield return new WaitForSeconds(_tripleShotActiveTime);
        _hasTripleShot = false;
    }

    public void Damage()
    {
        if (_hasShield)
        {
            _hasShield = false;
            _shield.SetActive(false);
            return;
        }
        _lives--;
        _UIManager.UpdateLives((_isPlayer1 ? 1 : 2),_lives);
        switch (_lives)
        {
            case 3:
                break;
            case 2:
                _rightEngine.SetActive(true);
                break;
            case 1:
                _leftEngine.SetActive(true);
                break;
            case 0:
                _spawnManager.OnPlayerDeath();
                _gameManager.GameOver();
                _UIManager.SaveHighScore();
                Destroy(this.gameObject);
                break;
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        _UIManager.UpdatePlayerScore(_score);
    }
}
