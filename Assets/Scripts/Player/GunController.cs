using UnityEngine;

public class GunController : MonoBehaviour
{
    #region Fields

    [SerializeField] private HookTrigger _hookTrigger;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    #endregion

    #region Unity Callbacks

    private void Start()
    {
        _hookTrigger = GetComponentInChildren<HookTrigger>();
    }

    #endregion

    #region Public Methods

    public void Aim(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float rotationAmount;
       
        Vector3 localScaleTemp = transform.localScale;
        localScaleTemp.x = angle > 90 || angle < -90 ? -1 : 1;
        rotationAmount = angle > 90 || angle < -90 ? 180 : 0;
        angle = angle > 90 || angle < -90 ? angle * -1 : angle * 1;
        transform.rotation = Quaternion.Euler(rotationAmount, 0, angle);
        transform.localScale = localScaleTemp;
    }


    public void Shoot(Vector3 mousePosition)
    {
        _hookTrigger.LaunchHook(mousePosition);

        if (_hookTrigger.isHooked)
        {
            _audioSource.PlayOneShot(_audioClip);
        }
    }

    public void DetachHook()
    {
        _hookTrigger.DetachHook();
    }

    #endregion
}