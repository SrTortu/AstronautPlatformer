using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    #region Fields

    [SerializeField] private Jetpack _jetpack;
    [SerializeField] private Player _player;
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

    void Update()
    {
        _energySlider.value = _jetpack.Energy;
        _textSlider.text = ((int)_jetpack.transform.position.y).ToString();

        if (!_jetpack.Flying && !_player.isDamage && Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.PlayOneShot(_noEnergySound);
            StartCoroutine(ShowAlert());
        }
    }

    #endregion


    #region Private Methods

    IEnumerator ShowAlert()
    {
        _energyAlert.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        _energyAlert.gameObject.SetActive(false);
    }

    #endregion
}