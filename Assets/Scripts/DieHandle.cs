using UnityEngine;

public class DieHandle : MonoBehaviour {
    [SerializeField] Canvas gameOverCanvas;

    private void Start() {
        
    }

    public void HandleDie() {

        Invoke("Stop",2f)
        ;
    }
    void Stop() {

        for(int i = 0 ; i < gameOverCanvas.transform.childCount ; i++) {
            gameOverCanvas.transform.GetChild(i).gameObject.SetActive(false);
        }


        Transform menuTransform = gameOverCanvas.transform.Find("Menu");
        if(menuTransform != null) {
            menuTransform.gameObject.SetActive(true);
        }

        Time.timeScale = 0;
    }
}
