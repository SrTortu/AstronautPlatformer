using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class end : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float endForce;
    private Rigidbody2D _playerRB;
    private ItemSpawner spawner;
    private void Update()
    {
        if (_playerRB != null)
            _playerRB.AddForce(Vector2.up * endForce * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if(spawner != null)
            {
                spawner = FindObjectOfType<ItemSpawner>();
                spawner.enabled = false;
            }


        }
    }
}
