using UnityEngine;
using System;

public class ItemPositive : Item
{
	#region Contants

	const float POSITIVE_HEAL = 0.1f;
	#endregion
	#region Unity Callbacks
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			Destroy(gameObject);

		if (collision.gameObject.tag == "Player")
		{
			Jetpack jetpack = collision.gameObject.GetComponent<Jetpack>();			
			jetpack.AddRegRatio(POSITIVE_HEAL);
			Recolected();
		}
	}
	#endregion

}
