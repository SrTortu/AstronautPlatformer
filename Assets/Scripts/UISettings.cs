using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;

public class UISettings : MonoBehaviour
{
	#region Properties
	#endregion

	#region Fields

	[SerializeField] Button _closeButton;
	[SerializeField] TMP_Dropdown _qualityDrop;
	[SerializeField] Toggle _vsyncToggle;
	[SerializeField] Toggle _fullScreenToggle;
	[SerializeField] Toggle _noShadowToggle;
	[SerializeField] Toggle _softShadowToggle;
	[SerializeField] Toggle _hardShadowToggle;
	[SerializeField] Slider _particlesResolutionSlider;

	#endregion

	#region Unity Callbacks
	// Start is called before the first frame update
	void Start()
    {
		//Events
		//Button click
		_closeButton.onClick.AddListener(CloseSettings);
		_qualityDrop.onValueChanged.AddListener(SetQuality);
		_vsyncToggle.onValueChanged.AddListener(SetVSync);
		//FullScreen
		//Screen.fullScreen = true;
		_particlesResolutionSlider.onValueChanged.AddListener(SetParticleResolution);
		_noShadowToggle.onValueChanged.AddListener(SetNoShadows);
		_noShadowToggle.onValueChanged.AddListener(SetSoftShadows);

		InitializeDropDownQuality();
		//ShadowResolution res = ShadowResolution.Low;

	}

	#endregion

	#region Public Methods
	#endregion

	#region Private Methods
	private void InitializeDropDownQuality()
	{
		List<string> options = new List<string>(QualitySettings.names);
		_qualityDrop.ClearOptions();
		_qualityDrop.AddOptions(options);

		// Configura el nivel de calidad actual como la opción seleccionada
		_qualityDrop.value = QualitySettings.GetQualityLevel();
		_qualityDrop.RefreshShownValue();
	}

	private void SetQuality(int index)
	{
		QualitySettings.SetQualityLevel(index, true);
	}

	private void SetVSync(bool stateOn)
	{
		if (stateOn)
			QualitySettings.vSyncCount = 1;
		else
			QualitySettings.vSyncCount = 0;
	}
	private void SetParticleResolution(float level)
	{
		QualitySettings.particleRaycastBudget = (int)level;
	}
	private void SetNoShadows(bool stateOn)
	{
		if (stateOn)
			QualitySettings.shadows = ShadowQuality.Disable;
	}
	private void SetSoftShadows(bool stateOn)
	{
		if (stateOn)
			QualitySettings.shadows = ShadowQuality.All;//Soft			
			//QualitySettings.shadows = ShadowQuality.HardOnly;//hard
	}
	private void CloseSettings()
	{
		gameObject.SetActive(false);
	}
	#endregion
}
