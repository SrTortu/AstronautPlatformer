using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlataformColisionController : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D platformCollider;
    private PlatFormDissapear _platFormDissapear;
    private bool _isInvisible;
   

    
    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        _isInvisible = TryGetComponent<PlatFormDissapear>(out _platFormDissapear);
      
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la posicion Y de player es superior a la de la plataforma le permite pararse sobre ella
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerEnteredPlatformTrigger(this, collision);
        }
        
        //verifica que la plataforma tenga dispoible el script para desaparecer 
        if(_isInvisible)
        {
            //Si el jugador se encuentra cerca a la plataforma esta activa su evento especial
            if (collision.CompareTag("PlayerChunk"))
            {
                _platFormDissapear.isActive = true;
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //Si la posicion Y de player es inferior a la de la plataforma le permite atravezarla
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerExitedPlatformTrigger(this, collision);
        }
        if (_isInvisible)
        {
            //Si el jugador se encuentra lejos a la plataforma esta desactiva su evento especial
            if (collision.CompareTag("PlayerChunk"))
            {
                _platFormDissapear.isActive = false;
            }

        }
    }

    public Collider2D PlatformCollider
    {
        get { return platformCollider; }
        set { platformCollider = value; }
    }
}

