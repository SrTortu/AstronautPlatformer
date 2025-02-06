using UnityEngine;
using System.Collections;
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
	[SerializeField] private TextMeshProUGUI _energyAlert;
	[SerializeField] private AudioSource _audioSource;
	[SerializeField] private AudioClip _noEnergySound;
	
    #endregion

    #region Unity Callbacks

    private void Start()
    {
		_audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
		_energySlider.value = _jetpack.Energy;
		_textSlider.text = ((int)_jetpack.transform.position.y).ToString();

		if(!_jetpack.Flying && Input.GetKeyDown(KeyCode.Space))
        {
			_audioSource.PlayOneShot(_noEnergySound);
			StartCoroutine(ShowAlert());
        }	
			
	}
	#endregion

	#region Public Methods
	
	IEnumerator ShowAlert()
	{
		_energyAlert.gameObject.SetActive(true); // Mostrar alerta
		yield return new WaitForSeconds(1f); // Esperar 1 segundo
		_energyAlert.gameObject.SetActive(false); // Ocultar alerta
	}
	#endregion

	#region Private Methods
	#endregion
}
