using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatformColisionController : MonoBehaviour
{
    private Collider2D _platformCollider;
    private PlatFormDissapear _platFormDissapear;
    private bool _isInvisible;


    private void Awake()
    {
        _platformCollider = GetComponent<Collider2D>();

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
        if (_isInvisible)
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
        get { return _platformCollider; }
        set { _platformCollider = value; }
    }
}