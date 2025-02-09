using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class PlatFormDissapear : MonoBehaviour
{
    public bool isActive;
    public bool isHide;
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
        isHide = false;
        _corrutineIsOn = false;

    }
    void Update()
    {
      if(!_corrutineIsOn) //se verifica que no se ejecuten corrutinas inesperadas
        {
            StartCoroutine(MakeInvisible());
        }
    }
    IEnumerator MakeInvisible()
    {
        _corrutineIsOn = true;
        while (isActive) // se verifica que se pueda activar la corrutina en caso de que el player este cerca 
        {
            Debug.Log("Trabajando");
            yield return new WaitForSeconds(Random.Range(3.7f,6.5f)); //Tiempo que toma en desaparecer
            foreach (Collider2D i in _platformColliders) // Se apagan los colliders de la plataforma
            {
                i.enabled = false;
            }
            _platformSprite.enabled = false;
            _platformLight.enabled = false;
            isHide = true;
            yield return new WaitForSeconds(Random.Range(1f,1.7f)); //tiempo que toma en volver a aparecer
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
}
