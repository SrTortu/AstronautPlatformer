using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndMessage : MonoBehaviour
{
    #region Fields

    private float currentAlpha;
    private float timer;
    private Color textColor;

    public TextMeshProUGUI textMeshPro;
    public float fadeDuration;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        textColor = textMeshPro.color;
        textColor.a = 0f;
        timer = 0;
        currentAlpha = 0f;
        textMeshPro.color = textColor;
    }

    private void FixedUpdate()
    {
        timer += Time.deltaTime;


        if (timer < fadeDuration)
        {
            currentAlpha = Mathf.Clamp01(timer / fadeDuration);
        }
        else
        {
            currentAlpha = 1f;
        }
        textColor.a = currentAlpha;
        textMeshPro.color = textColor;
    }

    #endregion
}