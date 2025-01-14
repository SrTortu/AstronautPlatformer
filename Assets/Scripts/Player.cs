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
	[SerializeField] private float _moveForce;
	private Animator _anim;
	private Rigidbody2D _targetRB;
	public SpriteRenderer _playerSprite;

   
    #endregion

    #region Unity Callbacks
    private void Awake()
	{
		_anim = GetComponent<Animator>();
		_targetRB = GetComponent<Rigidbody2D>();
		_playerSprite = GetComponent<SpriteRenderer>();
		
		
	}

	public void Walk(Direction walkDirection)
	{
		if (_jetpack.Flying)
			return;
		if (walkDirection == Direction.Left)
			_targetRB.AddForce(Vector2.left * _moveForce, ForceMode2D.Impulse);
		if (walkDirection == Direction.Right)
        {
			_targetRB.AddForce(Vector2.right * _moveForce, ForceMode2D.Impulse);

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
