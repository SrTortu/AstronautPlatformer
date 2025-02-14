using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CursorType : ScriptableObject
{
    #region Fields

    public Texture2D cursorTexture;
    public Vector2 cursorHotSpot;

    #endregion
}