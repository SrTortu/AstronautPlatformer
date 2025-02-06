using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlataformColisionController : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D platformCollider;
    private PlatFormDissapears _platFormDissapears;
    


    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
        _platFormDissapears = GetComponent<PlatFormDissapears>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerEnteredPlatformTrigger(this, collision);
        }
        if(_platFormDissapears != null)
        {
            if (collision.CompareTag("PlayerChunk"))
            {
                _platFormDissapears.isActive = true;
            }

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerExitedPlatformTrigger(this, collision);
        }
        if (_platFormDissapears != null)
        {
            if (collision.CompareTag("PlayerChunk"))
            {
                _platFormDissapears.isActive = false;
            }

        }
    }

    public Collider2D PlatformCollider
    {
        get { return platformCollider; }
        set { platformCollider = value; }
    }
}

