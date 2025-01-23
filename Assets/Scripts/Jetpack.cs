using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Jetpack : MonoBehaviour
{
	public enum Direction
	{
		Left,
		Right
	}

	#region Properties
	public float Energy 
	{
		get
		{
			return _energy;
		}
		set
		{
			_energy = Mathf.Clamp(value,0,_maxEnergy);
		}
	}
	public bool Flying { get; set; }
	
	#endregion

	#region Fields							     
	private Rigidbody2D _targetRB;
	[SerializeField] private float _energy;
	[SerializeField] private float _maxEnergy;
	[SerializeField] private float _energyFlyingRatio;
	[SerializeField] private float _energyRegenerationRatio;
	[SerializeField] private float _horizontalForce;
	[SerializeField] private float _jumForce;
	

	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_targetRB = GetComponent<Rigidbody2D>();
	}
	// Start is called before the first frame update
	void Start()
	{
		Energy = _maxEnergy;
	}

	// Update is called once per physic frame
	void FixedUpdate()
	{
		
			Regenerate();
	}

	#endregion

	#region Public Methods
	public void FlyUp()
	{
		Flying = true;
		DoFly();
	}
	public void StopFlying()
	{
		Flying = false;
	}

	public void Regenerate()
	{		
		Energy += _energyRegenerationRatio;
	}

	public void AddEnergy(float energy)
	{
		Energy += energy;
	}

	public void FlyHorizontal(Direction flyDirection)
	{
		if (!Flying)
			return;

		if (flyDirection == Direction.Left)
			_targetRB.AddForce(Vector2.left * _horizontalForce);
		else
			_targetRB.AddForce(Vector2.right * _horizontalForce);

	}

	#endregion

	#region Private Methods
	private void DoFly()
	{
		if (Energy > _energyFlyingRatio-1)
		{
			_targetRB.AddForce(Vector2.up * _jumForce, ForceMode2D.Impulse);
			Energy -= _energyFlyingRatio;
		}
		else
			Flying = false;
		
	}
	#endregion
}


