using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlataformColisionController : MonoBehaviour
{
    // Start is called before the first frame update

    private Collider2D platformCollider;
    


    private void Awake()
    {
        platformCollider = GetComponent<Collider2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerEnteredPlatformTrigger(this, collision);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InGameController.InstanceController.PlayerExitedPlatformTrigger(this, collision);
        }
    }

    public Collider2D PlatformCollider
    {
        get { return platformCollider; }
        set { platformCollider = value; }
    }
}

