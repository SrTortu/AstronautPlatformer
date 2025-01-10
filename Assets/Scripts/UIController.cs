using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields
	[SerializeField] private Jetpack _jetpack;
	[SerializeField] private Slider _energySlider;
	[SerializeField] private TextMeshProUGUI _textSlider;
	#endregion

	#region Unity Callbacks


    // Update is called once per frame
    void Update()
    {
		_energySlider.value = _jetpack.Energy;
		_textSlider.text = ((int)_jetpack.transform.position.y).ToString();

	}
	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	#endregion   
}
