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
		if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.JoystickButton6))
		{
            //Debug.Log("hej");
            //isActive = !isActive;
            //inventoryMenu.SetActive(true);
            //inventoryArrow.SetActive(false);

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
		//if (isActive) {
		//	//player.GetComponent<playerMovement>().canMove = false;
  //          Pause();

  //      }
		//if (!isActive) {
			////player.GetComponent<playerMovement>().canMove = true;
        //    Resume();
        //}

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
