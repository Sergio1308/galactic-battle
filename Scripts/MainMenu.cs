using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public Text progressText;

    public void PlayGame()
    {
        StartCoroutine(LoadAsynchronously());
    }

    public void QuitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quitting game");
        Application.Quit();
    }

    IEnumerator LoadAsynchronously ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("main", LoadSceneMode.Single);

        loadingScreen.SetActive(true);

        while (operation.isDone == false)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            Mathf.RoundToInt(progress);
            progressText.text = (progress * 100f).ToString("F0") + "%"; ;
            
            Debug.Log((progress * 100f).ToString("F0") + "%");

            yield return null;
        }
    }
}
