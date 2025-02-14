using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    #region Enum

    public enum Direction
    {
        Left,
        Right
    }

    #endregion

    #region Fields

    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private float _moveSpeed;

    private Animator _anim;
    private Rigidbody2D _targetRB;
    private bool _isWalking;

    public GunController playerGun;
    public bool isDamage;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        isDamage = false;
        _anim = GetComponent<Animator>();
        _targetRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _anim.SetBool("Flying", _jetpack.Flying);
        _anim.SetBool("Walking", _isWalking);
        _anim.SetBool("Damage", isDamage);
    }

    #endregion

    #region Public Methods

    public void Walk(Direction walkDirection)
    {
        if (_jetpack.Flying)
        {
            return;
        }

        if (walkDirection == Direction.Left)
        {
            _targetRB.AddForce(Vector2.left * _moveSpeed, ForceMode2D.Impulse);
        }

        if (walkDirection == Direction.Right)
        {
            _targetRB.AddForce(Vector2.right * _moveSpeed, ForceMode2D.Impulse);
        }
    }

    public void WalkOn()
    {
        _isWalking = true;
    }

    public void WalkOff()
    {
        _isWalking = false;
    }

    public void DamageOn()
    {
        StartCoroutine(GetDamage());
    }

    public void FlipPlayer(Direction direction)
    {
        Vector3 scale = transform.localScale;
        if (direction == Direction.Left)
        {
            scale.x = -1;
        }
        else
        {
            scale.x = 1;
        }

        transform.localScale = scale;
    }

    public void Shoot(Vector3 mouseDirection)
    {
        playerGun.Shoot(mouseDirection);
    }

    #endregion

    #region Private Methods

    IEnumerator GetDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
    }

    #endregion
}