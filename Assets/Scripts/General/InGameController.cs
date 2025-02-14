using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    public static InGameController InstanceController;

    #region Unity Callbacks

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }

    private void Awake()
    {
        if (InstanceController == null)
        {
            InstanceController = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Public Methods

    public void PlayerEnteredPlatformTrigger(PlatformColisionController platform, Collider2D player)
    {
        if (player.transform.position.y < platform.transform.position.y)
        {
            Physics2D.IgnoreCollision(player, platform.PlatformCollider, true);
        }
    }

    public void PlayerExitedPlatformTrigger(PlatformColisionController platform, Collider2D player)
    {
        Physics2D.IgnoreCollision(player, platform.PlatformCollider, false);
    }

    #endregion
}