using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }

    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private float _moveSpeed;
    private Animator _anim;
    private Rigidbody2D _targetRB;
    public GunController PlayerGun;
    public bool isDamage;
    private bool _isWalking;

    private void Awake()
    {
        isDamage = false;
        _anim = GetComponent<Animator>();
        _targetRB = GetComponent<Rigidbody2D>();
        PlayerGun = GetComponentInChildren<GunController>();
    }

    void Update()
    {
        _anim.SetBool("Flying", _jetpack.Flying);
        _anim.SetBool("Walking", _isWalking);
        _anim.SetBool("Damage", isDamage);
    }

    public void Walk(Direction walkDirection)
    {
        if (_jetpack.Flying) //Si esta volando no puede volar
            return;
        if (walkDirection == Direction.Left)
            _targetRB.AddForce(Vector2.left * _moveSpeed, ForceMode2D.Impulse);
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

    public void DagameOn()
    {
        StartCoroutine(MakeDamage());
    }

    public void Flip(Direction direction) //Hace que el player cambie visualmente de direccion
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
        PlayerGun.Shoot(mouseDirection);
    }

    IEnumerator MakeDamage()
    {
        isDamage = true;
        yield return new WaitForSeconds(1f);
        isDamage = false;
    }
}