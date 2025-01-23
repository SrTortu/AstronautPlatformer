using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    
    public static InGameController InstanceController; 

    #region Unity Callbacks


    // Update is called once per frame
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
    public void PlayerEnteredPlatformTrigger(PlataformColisionController platform, Collider2D player)
    {
       
        if (player.transform.position.y < platform.transform.position.y)
        {
            Physics2D.IgnoreCollision(player, platform.PlatformCollider, true);
           
        }
    }

    public void PlayerExitedPlatformTrigger(PlataformColisionController platform, Collider2D player)
    {
        
        Physics2D.IgnoreCollision(player, platform.PlatformCollider, false);
       
    }
    
}
