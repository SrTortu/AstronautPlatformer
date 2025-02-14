using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;


public class HookTrigger : MonoBehaviour
{
    #region Fields

    [SerializeField] private LayerMask _platformLayer;
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private float _hookStrengh;
    [SerializeField] private float _hookRange;

    private PlatFormDissapear _platFormDissapear;
    private Transform _initialHookPosition;
    private Vector2 _relativePosition;
    private Vector3 _hookPoint;
    private Vector3 _hookDirection;
    private Transform _hookedPlatform;
    private RaycastHit2D _hit;
    private float _hookStrenghAlter;
    private bool _isSpecial;
    private Dictionary<Collider2D, Light2D> _platformLights;
    private List<Collider2D> _platformsInRange;


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
        _platformLights = new Dictionary<Collider2D, Light2D>();
        _platformsInRange = new List<Collider2D>();
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

    private void Update()
    {
        CheckHookablePlatform();
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
            hookLine.enabled = true;
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
        if (_hookStrengh == 0)
        {
            _hookStrengh = _hookStrenghAlter;
        }

        _playerRB.AddForce(_hookDirection * _hookStrengh, ForceMode2D.Force);
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
            Physics2D.Raycast(initialPosition.position, _hookDirection, _hookRange, _platformLayer);
        return hit;
    }

    private void CheckHookablePlatform()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _hookRange, _platformLayer);
        List<Collider2D> currentPlatformsInRange = new List<Collider2D>();
        foreach (Collider2D platform in hitColliders)
        {
            if (!_platformLights.ContainsKey(platform))
            {
                _platformLights[platform] = platform.GetComponent<Light2D>();
            }

            if (_platformLights[platform] != null)
            {
                _platformLights[platform].enabled = true;
            }

            currentPlatformsInRange.Add(platform);
        }

        foreach (Collider2D platform in _platformsInRange)
        {
            if (!currentPlatformsInRange.Contains(platform))
            {
                if (_platformLights[platform] != null)
                {
                    _platformLights[platform].enabled = false;
                }
            }
        }

        _platformsInRange = currentPlatformsInRange;
    }

    #endregion
}