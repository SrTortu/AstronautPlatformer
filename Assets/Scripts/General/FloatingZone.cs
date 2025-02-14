using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingZone : MonoBehaviour
{
    #region Fields

    [SerializeField] private float floatingForce;
    private Rigidbody2D _playerRB;
    private ItemSpawner spawner;

    #endregion

    #region Unity Methods

    private void Start()
    {
        floatingForce = 19000;
    }

    private void Update()
    {
        if (_playerRB != null)
        {
            _playerRB.AddForce(Vector2.up * floatingForce * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (spawner != null)
            {
                spawner = FindObjectOfType<ItemSpawner>();
                spawner.enabled = false;
            }
        }
    }

    #endregion
}