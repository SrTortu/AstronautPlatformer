using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    
    [SerializeField]private LayerMask platformLayer;
    [SerializeField]private Rigidbody2D _playerRB;
    [SerializeField]private float _hookStrengh;
    [SerializeField]private float _hookMaxDistance;
    public LineRenderer hookLine;
    public bool isHooked;
    private PlatFormDissapear _platFormDissapear;
    private Transform _initialHookPosition;
    private Vector2 _relativePosition;
    private Vector3 _hookPoint;
    private Vector3 _hookDirection;
    private Transform _hookedPlatform;
    private RaycastHit2D _hit;
    private float _hookStrenghAlter;
    private bool isSpecial;
   
    void Start()
    {
        _initialHookPosition = this.transform;
        hookLine = GetComponent<LineRenderer>();
        isHooked = false;
        isSpecial = false;
        _playerRB = GetComponentInParent<Rigidbody2D>();
        hookLine.enabled = false;
    }

    public void launchHook(Vector3 mousePosition)
    {
        _initialHookPosition = this.transform;
        _hookDirection = mousePosition - this.transform.position;
        _hit = launchHit(_initialHookPosition);   

        if (_hit.collider != null) // Si golpea una plataforma
        {
            _hookedPlatform = _hit.collider.transform;
            _relativePosition = _hit.point - (Vector2)_hookedPlatform.position;
            _hookPoint = _hit.point;
            isHooked = true;
            _hookPoint = _hit.point;
            // Dibujar la cuerda
            hookLine.enabled = true;
            //Captura el script de la plataforma golpeada para corregir interacciones 
            isSpecial = _hit.collider.gameObject.TryGetComponent<PlatFormDissapear>(out _platFormDissapear);
        }
    }
    public void MoveTowardsHook()
    {

        _hookDirection = getHookDirection();
        //Evita que el player se quede quieto al estar enganchado
        if (_hookStrengh == 0)
            _hookStrengh = _hookStrenghAlter;
        _playerRB.AddForce(_hookDirection * _hookStrengh, ForceMode2D.Force);
        //Reduce la la aplicaciond de fuerza en caso de que la altura del player supere la plataforma
        if (this.transform.position.y > _hookPoint.y)
        {
            _hookStrenghAlter = _hookStrengh;
            _hookStrengh = 0;
        }





    }

    private void updateHook ()
    {
        if (_hookedPlatform != null && _hookPoint !=null)
        {
            _hookPoint = (Vector2)_hookedPlatform.position + _relativePosition;
            
        }
    }
    private Vector3 getHookDirection ()
    {
        Vector3 HookDirection = _hookPoint - this.transform.position;
        return HookDirection;
    }

    private RaycastHit2D launchHit (Transform initialPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(initialPosition.position, _hookDirection, _hookMaxDistance, platformLayer);
        return hit;
    }
    
    public void DetachHook()
    {
        isHooked = false;
        hookLine.enabled = false; // Ocultar la cuerda
    }
    private void FixedUpdate()
    {
        if(isSpecial) //Verifica si la plataforma tiene algun comportamiento especial
        {
            if(_platFormDissapear.isHide)// Si la plataforma se hace invisible se retira el gancho
            DetachHook();
        }
        if (isHooked)
        {
            MoveTowardsHook();
            updateHook();    

        }


        // Update rope

        hookLine.SetPosition(0, this.transform.position);
        hookLine.SetPosition(1, _hookPoint);
    }

}
