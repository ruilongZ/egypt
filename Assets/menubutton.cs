using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menubutton : MonoBehaviour
{
    public void startscene() {
        SceneManager.LoadScene("start");
    }
    public void quit() {
        Application.Quit();
    }
    public void developer() {
        SceneManager.LoadScene("developer");
    }

    public void backtomenu()
    {
        SceneManager.LoadScene("menu");
    }
}
