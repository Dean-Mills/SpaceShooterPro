using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    private bool _down = false;
    void Update()
    {
        transform.Translate((_down ? Vector3.down : Vector3.up) * _speed * Time.deltaTime);
        if(!_down && transform.position.y >= 7)
        {
            Destroy();
        }
        if(_down && transform.position.y < -7)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        if (transform.parent != null)
        {
            Destroy(this.transform.parent.gameObject);
            return;
        }
        Destroy(gameObject);
    }

    public void SetDirectionDown()
    {
        _down = true;
    }
}
