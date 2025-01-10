using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{    

	#region Unity Callbacks


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
	#endregion
}
