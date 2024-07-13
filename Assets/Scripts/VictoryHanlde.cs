using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryHanlde : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;


    public void HandleVictory() {

        for(int i = 0 ; i < gameOverCanvas.transform.childCount ; i++) {
            gameOverCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }


        Transform menuTransform = gameOverCanvas.transform.Find("Victory");
        if(menuTransform != null) {
            menuTransform.gameObject.SetActive(true);
        }

        Invoke("Stop", 2f);
    }
    void Stop() {

        LoadNextScene();

        Time.timeScale = 0;


    }
    void LoadNextScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalScenes = SceneManager.sceneCountInBuildSettings;

        Time.timeScale = 1;
        if(currentSceneIndex + 1 < totalScenes) {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else {
            SceneManager.LoadScene(0); 
        }
    }
    private void Update() {
        //if(Input.GetKeyDown(KeyCode.I)) {
        //    LoadNextScene();
        //}else if (Input.GetKeyDown(KeyCode.K)) {
        //    HandleVictory();
        //}
    }
}
