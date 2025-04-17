using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntructionsController : MonoBehaviour
{
    #region Fields

    private bool _isPaused;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _isPaused = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isPaused = false;
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
        }

        if (_isPaused)
        {
            Time.timeScale = 0;
        }
    }

    #endregion
}