using UnityEngine;


public class HookTrigger : MonoBehaviour
{
    #region Fields

    [SerializeField] private LayerMask _platformLayer;
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private float _hookStrengh;
    [SerializeField] private float _hookMaxDistance;

    private PlatFormDissapear _platFormDissapear;
    private Transform _initialHookPosition;
    private Vector2 _relativePosition;
    private Vector3 _hookPoint;
    private Vector3 _hookDirection;
    private Transform _hookedPlatform;
    private RaycastHit2D _hit;
    private float _hookStrenghAlter;
    private bool _isSpecial;

    public LineRenderer hookLine;
    public bool isHooked;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _initialHookPosition = this.transform;
        hookLine = GetComponent<LineRenderer>();
        isHooked = false;
        _isSpecial = false;
        _playerRB = GetComponentInParent<Rigidbody2D>();
        hookLine.enabled = false;
    }

    private void FixedUpdate()
    {
        if (_isSpecial)
        {
            if (_platFormDissapear.isHide)
                DetachHook();
        }

        if (isHooked)
        {
            MoveTowardsHook();
            UpdateHook();
        }

        // Update rope
        hookLine.SetPosition(0, this.transform.position);
        hookLine.SetPosition(1, _hookPoint);
    }

    #endregion

    #region Public Methods

    public void LaunchHook(Vector3 mousePosition)
    {
        _initialHookPosition = this.transform;
        _hookDirection = mousePosition - this.transform.position;
        _hit = LaunchHit(_initialHookPosition);

        if (_hit.collider != null)
        {
            _hookedPlatform = _hit.collider.transform;
            _relativePosition = _hit.point - (Vector2)_hookedPlatform.position;
            _hookPoint = _hit.point;
            isHooked = true;
            _hookPoint = _hit.point;
            // Dibujar la cuerda
            hookLine.enabled = true;
            //Captura el script de la plataforma golpeada para corregir interacciones 
            _isSpecial = _hit.collider.gameObject.TryGetComponent<PlatFormDissapear>(out _platFormDissapear);
        }
    }

    

    public void DetachHook()
    {
        isHooked = false;
        hookLine.enabled = false;
    }

    #endregion

    #region Private Methods

    private void MoveTowardsHook()
    {
        _hookDirection = GetHookDirection();
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
    private void UpdateHook()
    {
        if (_hookedPlatform != null && _hookPoint != null)
        {
            _hookPoint = (Vector2)_hookedPlatform.position + _relativePosition;
        }
    }

    private Vector3 GetHookDirection()
    {
        Vector3 hookDirection = _hookPoint - this.transform.position;
        return hookDirection;
    }

    private RaycastHit2D LaunchHit(Transform initialPosition)
    {
        RaycastHit2D hit =
            Physics2D.Raycast(initialPosition.position, _hookDirection, _hookMaxDistance, _platformLayer);
        return hit;
    }
    #endregion
    
}