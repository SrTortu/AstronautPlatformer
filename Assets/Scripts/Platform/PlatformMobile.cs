using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMobile : MonoBehaviour
{
    [SerializeField]private float _speed;
    private Vector2 _pointA;
    private Vector2 _pointB;
    private Vector2 _target;
    private bool _isActive;
    
    void Start()
    {
        _pointA = new Vector2(-5, this.transform.position.y);
        _pointB = new Vector2(6, this.transform.position.y);
        _isActive = false;
        _target = (Random.Range(0, 2) == 0) ? _pointA : _pointB;
    }

    void Update()
    {
        if(_isActive)
        { 
            transform.position = Vector2.MoveTowards(transform.position, _target, _speed * Time.deltaTime);

            if(Vector2.Distance(transform.position, _target) < 0.1f)
            {
                _target = (_target == _pointA) ? _pointB : _pointA;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(this.CompareTag("EarthPlatform"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(this.transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (this.CompareTag("EarthPlatform"))
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.SetParent(null);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerChunk"))
        {
            _isActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerChunk"))
        {
            _isActive = false;
        }
    }
}
