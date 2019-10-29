using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleInventory : MonoBehaviour {

	bool isActive;
	public GameObject inventoryArrow;
	public GameObject inventoryMenu;
	public GameObject player;
    // Use this for initialization

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Start () {
		isActive = false;
		inventoryMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
/*         if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.JoystickButton7))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseMenu();
            }
        }

        if (Input.GetKeyDown(KeyCode.L) || Input.GetKeyDown(KeyCode.JoystickButton6))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                InvMenu();
            }
        } */

        if(Input.GetKeyDown(KeyCode.U))
        {
            LoadMenu();
        }
    }

    public void PauseMenu() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void InvMenu() {
        Time.timeScale = 0f;
        GameIsPaused = true;
        inventoryMenu.SetActive(true);
        inventoryArrow.SetActive(false);
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        inventoryMenu.SetActive(false);
        inventoryArrow.SetActive(true);

    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        inventoryMenu.SetActive(true);
        inventoryArrow.SetActive(false);
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene(0);
    }
}
