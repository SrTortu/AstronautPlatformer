using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region Fields

    [SerializeField] Button _startGameButton;
    [SerializeField] Button _exitGameButton;
    [SerializeField] Button _dontTouchMeButton;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _startGameMusic;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _exitGameButton.onClick.AddListener(ExitGame);
        _dontTouchMeButton.onClick.AddListener(FreeWillScreen);
        EventSystem.current.SetSelectedGameObject(null);
    }


    private void Update()
    {
        if (!_audioSource.isPlaying)
        {
            _audioSource.volume = 0.13f;
            _audioSource.PlayOneShot(_startGameMusic);
        }
    }

    #endregion

    #region Private Methods

    private void FreeWillScreen()
    {
        SceneManager.LoadScene("FreeWill");
    }
    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartGame()
    {
        SceneManager.LoadScene("InGame");
    }

    #endregion
}