using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    #region Fields

    [SerializeField] Button _startGameButton;
    [SerializeField] Button _exitGameButton;

    #endregion

    #region Unity Callbacks

    void Start()
    {
        _startGameButton.onClick.AddListener(StartGame);
        _exitGameButton.onClick.AddListener(ExitGame);
    }

    #endregion

    #region Private Methods

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