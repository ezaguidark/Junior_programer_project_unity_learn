using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemyCars;
    public int enemyToSpawn;
    private int score;
    public bool gameOver;
    private bool paused;
    private bool menuPageAct;
    public bool fullScreen;
    // Se necesita importar TMPro
    public TextMeshProUGUI scoreText;
    public GameObject pauseScreen;
    public GameObject inGameScreen;
    public GameObject mainMenu;
    public GameObject howToPlay;
    public GameObject configMenu;
    public GameObject htpPag1;
    public GameObject htpPag2;
    public GameObject gameOverScreen;
    public GameObject player;
    public GameObject creditsMenu;
    public GameObject menuCamera;
    public TMP_Dropdown livesOptions;
    public GameObject menuMusic;
    public GameObject gameMusic;
    
    // Start is called before the first frame update
    
    void Start()
    {
         Resolution[] resolutions = Screen.resolutions;

        // Print the resolutions
        foreach (var res in resolutions)
        {
            Debug.Log(res.width + "x" + res.height + " : " + res.refreshRate);
        }
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }

    public void StartGame()
    {   
        // primero debe estar gameover en false.
        // el sccript del jugador debe estar activo. lo desactive para que no se moviera el player en el menu xd
        gameOver = false;
        player.GetComponent<VehicleControl>().enabled = true;
        SpawnEnemies(enemyToSpawn);
        score = 0;
        scoreText.text = score.ToString();
        inGameScreen.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        menuCamera.gameObject.SetActive(false);
        gameMusic.gameObject.SetActive(true);
        menuMusic.gameObject.SetActive(false);
        
        
    }

    public void ReStartGame()
    {
        // Hasta la fecha, es la unica forma que se de re-iniciar una escena.
        // Creo que vuelve a cargar la escena actual, solo sirve si el juego tiene una sola escena.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;

    }

    // Update is called once per frame
    void Update()
    {
        int enemyInScene = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyInScene < 5 & gameOver == false)
        {
            enemyToSpawn *= 2;
            SpawnEnemies(enemyToSpawn);
        }

        if (Input.GetKeyDown(KeyCode.Escape) & gameOver == false)
        {
            PauseGame();
        }

        menuCamera.transform.Rotate(Vector3.up * 15 * Time.deltaTime);

    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomX = Random.Range(-400, 400);
        float randomZ = Random.Range(-400, 400);
        Vector3 spawnPos = new Vector3(randomX, 0, randomZ);
        return spawnPos;
    }

    void SpawnEnemies(int number)
    {
        if (gameOver == false)
        {
            for (int i = 0; i < number; i++)
            {
                int randomIndex = Random.Range(0,4);
                Instantiate(enemyCars[randomIndex], GenerateSpawnPosition(), enemyCars[randomIndex].transform.rotation);
            }
        }
        
    }

    void DestroyAllEnemies()
    {
        GameObject[] enemigosRestantes = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemigosRestantes)
        {
            Destroy(enemy.gameObject);
        }
    }

    public void GameOver()
    {
        gameOver = true;
        DestroyAllEnemies();
        gameOverScreen.gameObject.SetActive(true);
        inGameScreen.gameObject.SetActive(false);
    }

    public void AddScore()
    {
        score += 1; 
        scoreText.text = score.ToString();
    }

    public void PauseGame()
    {
        paused = !paused;
        pauseScreen.gameObject.SetActive(paused);
        if (paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ReturnMainMenu()
    {
        mainMenu.gameObject.SetActive(true);
        howToPlay.gameObject.SetActive(false);
        configMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
    }

    public void HowToPlay()
    {
        howToPlay.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
        
        menuPageAct = !menuPageAct;

        htpPag1.gameObject.SetActive(menuPageAct);
        htpPag2.gameObject.SetActive(!menuPageAct);
    }

    public void SettingsMenu()
    {
        mainMenu.gameObject.SetActive(false);
        configMenu.gameObject.SetActive(true);
    }

    public void CreditsMenu()
    {
        mainMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(true);
    }

    public void TestResolusion()
    {
        fullScreen = !fullScreen;
        if (fullScreen == false)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
    public void SetPlayerLives()
    {
        if (livesOptions.value == 0)
        {
            player.gameObject.GetComponent<VehicleControl>().livePoints = 25;
        }
        if (livesOptions.value == 1)
        {
            player.gameObject.GetComponent<VehicleControl>().livePoints = 50;
        }
        if (livesOptions.value == 2)
        {
            player.gameObject.GetComponent<VehicleControl>().livePoints = 100;
        }
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }

}
