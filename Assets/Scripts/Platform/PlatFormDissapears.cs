using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlatFormDissapears : MonoBehaviour
{
    public bool isActive;
    private bool _corrutineIsOn;
    private Collider2D [] _platformColliders;
    private SpriteRenderer _platformSprite;
    private Light2D _platformLight;


    // Update is called once per frame
    private void Start()
    {
        _platformColliders = GetComponents<Collider2D>();
        _platformSprite = this.GetComponent<SpriteRenderer>();
        _platformLight = GetComponent<Light2D>();
        isActive = false;
        _corrutineIsOn = false;

    }
    void Update()
    {
      if(!_corrutineIsOn)
        {
            StartCoroutine(MakeInvisible());
        }
    }
    IEnumerator MakeInvisible()
    {
        _corrutineIsOn = true;
        while (isActive)
        {
            yield return new WaitForSeconds(5f);
            foreach (Collider2D i in _platformColliders)
            {
                i.enabled = false;
            }
            _platformSprite.enabled = false;
            _platformLight.enabled = false;
            yield return new WaitForSeconds(2f);
            foreach (Collider2D i in _platformColliders)
            {
                i.enabled = true;
            }
            _platformSprite.enabled = true;
            _platformLight.enabled = true;

        }
        _corrutineIsOn = false;
    }
}
