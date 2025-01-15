using UnityEngine;
using System;

public class InputController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Jetpack _jetpack;
	[SerializeField] private Player _player;
	private Vector3 mousePosition;
	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Horizontal Fly
		if (Input.GetAxis("Horizontal") < 0)	
        {
			_player.Flip(Player.Direction.Left);
			_jetpack.FlyHorizontal(Jetpack.Direction.Left);
        }
		if (Input.GetAxis("Horizontal") > 0)
        {
			_player.Flip(Player.Direction.Right);
			_jetpack.FlyHorizontal(Jetpack.Direction.Right);
        }

		//Vertical Fly
		if (Input.GetAxis("Vertical") > 0)
			_jetpack.FlyUp();
		else
			_jetpack.StopFlying();

		//Walk
		if ((Input.GetAxis("Vertical") == 0) &&  (Input.GetAxis("Horizontal") < 0))
		{
			_player.Walk(Player.Direction.Left);
			_player.WalkOn();
        }
		if ((Input.GetAxis("Vertical") == 0) && (Input.GetAxis("Horizontal") > 0))
		{
			_player.Walk(Player.Direction.Right);
			_player.WalkOn();

		}
		if (Input.GetAxis("Horizontal") == 0 || (Input.GetAxis("Vertical") > 0))
        {	
			_player.WalkOff();
        }

		//Aim
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (mousePosition.x > _player.transform.position.x)
		{
			_player.Flip(Player.Direction.Left);
			_player.PlayerGun.Aim(mousePosition);
		}
		
		else if (mousePosition.x < _player.transform.position.x)
		{
			_player.Flip(Player.Direction.Right);
			_player.PlayerGun.Aim(mousePosition);
		}
	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
