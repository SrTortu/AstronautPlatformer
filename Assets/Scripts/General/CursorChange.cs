using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    #region Fields

    [SerializeField] private CursorType _cursorDefault;
    [SerializeField] private CursorType _cursorGrab;

    private PlatformColisionController _platformColisionController;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _platformColisionController = GetComponent<PlatformColisionController>();
    }


    private void OnMouseExit()
    {
        Cursor.SetCursor(_cursorDefault.cursorTexture, _cursorDefault.cursorHotSpot, CursorMode.Auto);
    }

    private void OnMouseOver()
    {
        if (_platformColisionController.isHoockable)
        {
            Cursor.SetCursor(_cursorGrab.cursorTexture, _cursorGrab.cursorHotSpot, CursorMode.Auto);
        }
    }

    #endregion
}