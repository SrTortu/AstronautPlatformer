using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlatFormDissapear : MonoBehaviour
{
    #region Fields

    private bool _corrutineIsOn;
    private Collider2D[] _platformColliders;
    private SpriteRenderer _platformSprite;
    private Light2D _platformLight;

    public bool isActive;
    public bool isHide;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _platformColliders = GetComponents<Collider2D>();
        _platformSprite = this.GetComponent<SpriteRenderer>();
        _platformLight = GetComponent<Light2D>();
        isActive = false;
        isHide = false;
        _corrutineIsOn = false;
    }

    void Update()
    {
        if (!_corrutineIsOn)
        {
            StartCoroutine(MakeInvisible());
        }
    }

    #endregion

    #region Private Methods

    IEnumerator MakeInvisible()
    {
        _corrutineIsOn = true;
        while (isActive)
        {
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            foreach (Collider2D i in _platformColliders)
            {
                i.enabled = false;
            }

            _platformSprite.enabled = false;
            _platformLight.enabled = false;
            isHide = true;
            yield return new WaitForSeconds(Random.Range(1.3f, 2f));
            foreach (Collider2D i in _platformColliders)
            {
                i.enabled = true;
            }

            _platformSprite.enabled = true;
            _platformLight.enabled = true;
            isHide = false;
        }

        _corrutineIsOn = false;
    }

    #endregion
}