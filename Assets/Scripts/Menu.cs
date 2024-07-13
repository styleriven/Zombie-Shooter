using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void NewGame() {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void QuitGame() {
        Application.Quit();
    }
}
