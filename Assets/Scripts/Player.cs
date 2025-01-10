using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Jetpack _jetpack;
	private Animator _anim;
	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
		_anim.SetBool("Flying", _jetpack.Flying);
    }
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
