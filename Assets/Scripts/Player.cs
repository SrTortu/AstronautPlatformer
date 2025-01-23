using UnityEngine;
using System;

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
	public Transform PlayerSprite;
	public bool isOnGround;

   
    #endregion

    #region Unity Callbacks
    private void Awake()
	{
		_anim = GetComponent<Animator>();
		_targetRB = GetComponent<Rigidbody2D>();
		PlayerSprite = GetComponent<Transform>();
		PlayerGun = GetComponentInChildren<GunController>();
		
		
		
	}

	public void Walk(Direction walkDirection)
	{
		if (_jetpack.Flying)
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
	public void Flip(Direction direction)
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
	// Update is called once per frame
	void Update()
    {
		_anim.SetBool("Flying", _jetpack.Flying);
		_anim.SetBool("Walking", _walking);
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
