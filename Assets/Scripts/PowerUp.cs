using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType { TripleShot, Speed, Shield };
public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    [SerializeField]
    private PowerUpType _powerUpType;
    
    // Update is called once per frame
    void Update()
    {
        //move down at a speed of 3
        //when leave the screen destroy the object
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -4.5f)
        {
            Destroy(this.gameObject);
        }
    }

    //on triggerCollision
    //Only be collectable by the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
            var player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                switch (_powerUpType)
                {
                    case PowerUpType.TripleShot:
                        player.TripleShotCollected();
                        break;
                    case PowerUpType.Speed:
                        player.SpeedBoostCollected();
                        break;
                    case PowerUpType.Shield:
                        player.ShieldCollected();
                        break;
                    default:
                        Debug.Log("Defualt");
                        break;
                }
                
            }
        }
    }
}
