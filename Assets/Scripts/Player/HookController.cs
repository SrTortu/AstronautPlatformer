using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private LayerMask platformLayer;
    [SerializeField]private Rigidbody2D _playerRB;
    [SerializeField]private float _hookSpeed;
    [SerializeField]private float _hookMaxDistance;
    public LineRenderer hookLine;
    public LineRenderer lineTest;
    private Vector3 _hookPoint;
    private Transform _initialHookPosition;
    private Vector3 direction;
    private Vector3 _moveDirection;
    private RaycastHit2D _hit;
    private RaycastHit2D _hitTemp; // Temporal para detectar si el punto de enganche se ha desaparecido
    private float _hookSpeedAlter;

    private bool isHooked;
   
    void Start()
    {
        _initialHookPosition = this.transform;
        hookLine = GetComponent<LineRenderer>();
        lineTest = GetComponent<LineRenderer>();
        isHooked = false;
        _playerRB = GetComponentInParent<Rigidbody2D>();
        hookLine.enabled = false;
    }

    public void launchHook(Vector3 mousePosition)
    {
        Debug.Log("mouse button");
        _initialHookPosition = this.transform;
        direction = mousePosition - this.transform.position;
        _hit = launchHit(_initialHookPosition);
        

        if (_hit.collider != null) // Si golpea una plataforma
        {
            _hookPoint = _hit.point;
            isHooked = true;

            //// Dibujar la cuerda
            hookLine.enabled = true;
            hookLine.SetPosition(0, this.transform.position);
            hookLine.SetPosition(1, _hookPoint);
        }
    }
    public void MoveTowardsHook()
    {

        _moveDirection = getHookDirection();
        if (_hookSpeed == 0)
            _hookSpeed = _hookSpeedAlter;
        _playerRB.AddForce(_moveDirection * _hookSpeed, ForceMode2D.Force);
        if (this.transform.position.y > _hookPoint.y)
        {
            _hookSpeedAlter = _hookSpeed;
            _hookSpeed = 0;
        }





    }
    private Vector3 getHookDirection ()
    {
        Vector3 HookDirection = _hookPoint - this.transform.position;
        return HookDirection;
    }

    private RaycastHit2D launchHit (Transform initialPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(initialPosition.position, direction, _hookMaxDistance, platformLayer);
        return hit;
    }

    public void DetachHook()
    {
        isHooked = false;
        hookLine.enabled = false; // Ocultar la cuerda
    }
    private void Update()
    {
        _hit = launchHit(_initialHookPosition);
        if (_hit.collider == null)
            DetachHook();
        if (isHooked)
            MoveTowardsHook();

        // Update rope
        lineTest.SetPosition(0, this.transform.position);
        lineTest.SetPosition(1, _hitTemp.point);

        hookLine.SetPosition(0, this.transform.position);
        hookLine.SetPosition(1, _hookPoint);
    }

}
