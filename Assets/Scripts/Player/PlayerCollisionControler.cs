using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionControler : MonoBehaviour
{
    // Start is called before the first frame update
    private Player _player;
    void Start()
    {
        _player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Platform"))
        {
            _player.isOnGround = true;
        }
    }
 
  
}
