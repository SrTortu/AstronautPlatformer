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
    private Vector3 _hookPoint;
    private Vector3 _initialHookPosition;
    private Vector3 direction;
    private float _hookSpeedAlter;
    private bool isHooked;
    void Start()
    {
        hookLine = GetComponent<LineRenderer>();    
        isHooked = false;
        _playerRB = GetComponentInParent<Rigidbody2D>();
        hookLine.enabled = false;
    }

    // Update is called once per frame
    public void launchHook(Vector3 mousePosition)
    {
        
        direction = mousePosition - this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, direction, _hookMaxDistance, platformLayer);
        

        if (hit.collider != null) // Si golpea una plataforma
        {
            _hookPoint = hit.point;
            isHooked = true;

            //// Dibujar la cuerda
            hookLine.enabled = true;
           
            hookLine.SetPosition(0, this.transform.position);
            hookLine.SetPosition(1, _hookPoint);
        }
    }
    public void MoveTowardsHook()
    {

        direction = getHookDirection();
        if (_hookSpeed == 0)
            _hookSpeed = _hookSpeedAlter;

        Vector2 position = Vector2.MoveTowards(this.transform.position, _hookPoint, _hookSpeed * Time.deltaTime);
        _playerRB.AddForce(direction * _hookSpeed, ForceMode2D.Force);
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

    public void DetachHook()
    {
        isHooked = false;
        hookLine.enabled = false; // Ocultar la cuerda
    }
    private void Update()
    {
       if (isHooked)
            MoveTowardsHook();
        // Update rope

        hookLine.SetPosition(0, this.transform.position);
        hookLine.SetPosition(1, _hookPoint);
    }

}
