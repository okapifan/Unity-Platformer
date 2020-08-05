using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    public GameObject pm;
    public GameObject settings;
    public GameObject inventory;

    private bool isInventoryEnabled = false;
    private int allSlots;
    private int enabledSlots;
    private GameObject[] slot;
    public GameObject slotHolder;


    // Start is called before the first frame update
    void Start() {
        allSlots = 36;
        slot = new GameObject[allSlots];

        for (int i = 0; i < allSlots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i).gameObject;
        }
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
        else if (Input.GetKeyDown(KeyCode.E) && !isGamePaused)
        {
            if (!isInventoryEnabled)
            {
                inventory.SetActive(true);
                isInventoryEnabled = !isInventoryEnabled;
            }
            else
            {
                inventory.SetActive(false);
                isInventoryEnabled = !isInventoryEnabled;
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
        inventory.SetActive(false);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void quit() {
        Application.Quit();
    }

    public void Settings() {
        pm.SetActive(false);
        settings.SetActive(true);
        inventory.SetActive(false);
    }

    public void BackMenu() {
        settings.SetActive(false);
        pm.SetActive(true);
        inventory.SetActive(false);
    }
}
