using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale= 1;
        gameObject.SetActive(false);

        print("RESUMING");
    }

    public void quit()
    {
        Application.Quit();
        print("QUITTING");
    }
}
