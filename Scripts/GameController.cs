using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWave;
    public float waveWait;

    public GameObject scoreText;
    public GameObject waveTimeText;
    public GameObject restartText;
    public GameObject gameOverText;
    public GameObject restartButton;
    public GameObject menuButton;
    public GameObject quitButton;

    public bool gameOver;
    public bool healthy = false;
    private bool restart;
    private bool nextWaveText;

    private int nmbWave = 0;
    private int score = 0;
    private float timeStart = 10f;
    public Text Timer;
    public GameObject nextWave;
    private int waveTimer = 2;
    private float timeWaveCount = 0f;
    //private bool nextWaveSpawn = false;


    private void Start()
    {
        gameOver = false;
        restart = false;
        restartButton.SetActive(false);
        menuButton.SetActive(false);
        quitButton.SetActive(false);
        nextWaveText = false;
        nextWave.GetComponent<UnityEngine.UI.Text>().text = "";

        gameOverText.GetComponent<UnityEngine.UI.Text>().text = "";
        Timer.text = timeStart.ToString();

        //StartCoroutine(SpawnWaves());
    }

    public void Update()
    {
        if (waveTimer < startWave)
        {
            StartCoroutine(SpawnWaves());
            waveTimer += 1;
        }

        if (gameOver)
        {
            restartButton.SetActive(true);
            menuButton.SetActive(true);
            quitButton.SetActive(true);
            restart = true;
            healthy = true;
        }
        if (nextWaveText == true)
        {
            //nextWave.GetComponent<UnityEngine.UI.Text>().text = "Wave " + nmbWave;
        }

        if (startWave < Time.time && timeStart > 0 && gameOver == false)
        {
            nextWave.SetActive(false);
            timeStart -= Time.deltaTime;
            Timer.text = Mathf.Round(timeStart).ToString();
        }
        else
        {
            if (timeStart <= 0)
            {
                //nextWave.GetComponent<UnityEngine.UI.Text>().text = "Wave " + nmbWave;
                StartCoroutine(SpawnWaves());
                timeStart = 10f + timeWaveCount;
            }
        }
        //    if (restart)
        //    {
        //        if (input.getkeydown(keycode.r))
        //        {
        //            scenemanager.loadscene("main", loadscenemode.single); // additive for more scenes
        //        }
        //    }
    }

    IEnumerator SpawnWaves()
    {
        Debug.Log("start new wave");
        nextWaveText = true;
        nmbWave += 1;
        nextWave.GetComponent<UnityEngine.UI.Text>().text = "Wave " + nmbWave;
        yield return new WaitForSeconds(startWave);
        timeWaveCount += 10;

        //while (true)
        //{
        for (int i = 0; i < hazardCount; i++)
        {
            if (gameOver)
            {
                break;
            }
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
            Quaternion spawnRotation = Quaternion.identity;

            GameObject hazard = hazards[Random.Range(0, hazards.Length)];
            Instantiate(hazard, spawnPosition, spawnRotation);

            yield return new WaitForSeconds(Random.Range(0.5f, spawnWait));
        }
        yield return new WaitForSeconds(waveWait);
        Debug.Log("End wave");
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
        
    }

    public void GameOver()
    {
        gameOverText.GetComponent<UnityEngine.UI.Text>().text = "Game Over!";
        gameOver = true;

        GetComponent<AudioSource>().Stop();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("main", LoadSceneMode.Single); // additive for more scenes
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("menu");
    }
}