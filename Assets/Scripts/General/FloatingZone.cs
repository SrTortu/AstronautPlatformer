using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatingZone : MonoBehaviour
{
    #region Fields

    [SerializeField] private float floatingForce;
    [SerializeField] private ItemSpawner spawner;
    [SerializeField] private EndMessage _endMessage;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private Rigidbody2D _playerRB;
    private float _timer;
    private bool _isEnding;

    #endregion

    #region Unity Methods

    private void Start()
    {
        floatingForce = 19000;
        _isEnding = false;
    }

    private void FixedUpdate()
    {
        if (_playerRB != null)
        {
            _playerRB.AddForce(Vector2.up * floatingForce * Time.deltaTime);
        }
    }

    private void Update()
    {
        if (_isEnding)
        {
            _timer += Time.deltaTime;
            if (_timer > 15f)
            {
                SceneManager.LoadScene("MainMenu");
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            _playerRB.GetComponent<Jetpack>()._jumForce = 0;
            spawner.enabled = false;
            _endMessage.enabled = true;
            _audioSource.clip = _audioClip;
            _audioSource.volume = 0.2f;
            _audioSource.Play();
            _isEnding = true;
        }
    }

    #endregion
}