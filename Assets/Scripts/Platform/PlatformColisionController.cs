using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlatformColisionController : MonoBehaviour
{
    #region Properties

    public Collider2D PlatformCollider
    {
        get { return _platformCollider; }
        set { _platformCollider = value; }
    }

    #endregion

    #region Fields

    private Collider2D _platformCollider;
    private PlatFormDissapear _platFormDissapear;
    private bool _isInvisible;

    #endregion

    #region Unity Callbacks

    private void Awake()
    {
        _platformCollider = GetComponent<Collider2D>();

        _isInvisible = TryGetComponent<PlatFormDissapear>(out _platFormDissapear);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerEnteredPlatformTrigger(this, collision);
        }


        if (_isInvisible)
        {
            if (collision.CompareTag("PlayerChunk"))
            {
                _platFormDissapear.isActive = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerExitedPlatformTrigger(this, collision);
        }

        if (_isInvisible)
        {
            if (collision.CompareTag("PlayerChunk"))
            {
                _platFormDissapear.isActive = false;
            }
        }
    }

    #endregion
}