using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public bool paused;

    public GameObject pauseMenuUI;
    public GameObject gameController;
    public PlayerController playerController;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gameController.GetComponent<GameController>().healthy == false)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        paused = false;
        Debug.Log("can shoot");

        playerController.CalibrateAccelerometer();
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        paused = true;
        Debug.Log("can't shoot");
    }

    public void LoadMenu()
    {
        Debug.Log("Loading menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }
}
