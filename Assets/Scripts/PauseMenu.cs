using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pm;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isGamePaused) {
                resume();
            } else {
                pause();
            }
        }
    }

    public void resume() {
        pm.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    void pause() {
        pm.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void quit() {
        Application.Quit();
    }

    public void settings() {
        //Todo
    }
}
