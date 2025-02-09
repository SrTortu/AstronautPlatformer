using UnityEngine;
using System;
using UnityEngine.Rendering.Universal;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Player : MonoBehaviour
{
	#region Properties
	#endregion
	public enum Direction
	{
		Left,
		Right
	}

	#region Fields
	private bool _walking;
	

	[SerializeField] private Jetpack _jetpack;
	[SerializeField] private float _moveSpeed;
	private Animator _anim;
	private Rigidbody2D _targetRB;
	public GunController PlayerGun;
	public bool isOnGround;
	public bool isDamage;

   
    #endregion

    #region Unity Callbacks
    private void Awake()
	{
		isDamage = false;
		_anim = GetComponent<Animator>();
		_targetRB = GetComponent<Rigidbody2D>();
		PlayerGun = GetComponentInChildren<GunController>();
		
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
		_walking = true;
	}
	public void WalkOff()
	{
		_walking = false;
	}
	public void DagameOn ()
    {
		StartCoroutine (MakeDamage());
    }
	
	public void Flip(Direction direction) //Hace que el player cambie visualmente de direccion
    {
		
		Vector3 scale = transform.localScale;
		if (direction == Direction.Left)
        {
			scale.x = -1;
			PlayerGun.FlipShootPoint(0f);
        }
		else
        {
			scale.x = 1;
			PlayerGun.FlipShootPoint(180f);
		}
		transform.localScale = scale;
    }
	public void Shoot(Vector3 mouseDirection)
	{
		
		PlayerGun.shoot(mouseDirection);
	}
	IEnumerator MakeDamage ()
    {
		isDamage = true;
		yield return new WaitForSeconds(1f);
		isDamage = false;
    }
	// Update is called once per frame
	void Update()
    {
		_anim.SetBool("Flying", _jetpack.Flying);
		_anim.SetBool("Walking", _walking);
		_anim.SetBool("Damage", isDamage);
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
