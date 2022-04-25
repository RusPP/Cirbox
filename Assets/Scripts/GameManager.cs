using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> coloredBalls;
    private Vector3 spawnPosition;
    private float spawnPosXBound = 4.5f;
    private int ballIndex;
    [SerializeField] private float startDelay = 1f;
    [SerializeField] private float repeatRate = 1f;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private int lives;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button restartBtn;
    private AudioSource audioSource;
    public int score;
    private int combo;
    public bool gameIsOn;
    public bool isPaused = false;

    private void Awake()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && gameIsOn)
        {
            ChangePaused();
        }
    }

    private void SpawnBalls()
    {
        if (gameIsOn)
        {
            // Set a random spawn position on the X axis
            spawnPosition = new Vector3(Random.Range(-spawnPosXBound, spawnPosXBound), 14, 0);
            // Ball with a random color
            ballIndex = Random.Range(0, coloredBalls.Count);
            // Create colored ball from the list 
            Instantiate(coloredBalls[ballIndex], spawnPosition, transform.rotation);
        }
    }

    public void CountPoints(int points)
    {
        if (gameIsOn)
        {
            score += points + Mathf.FloorToInt(combo / 3);
            scoreText.text = "Score: " + score;
        }
        
    }

    public void ComboCounter(int multiplier)
    {
        if (gameIsOn)
        {
            combo = combo * multiplier;
            combo++;
            comboText.text = "Combo: " + Mathf.FloorToInt(combo / 3);
        }
    }

    public void StartGame()
    {
        gameIsOn = true;
        gameOverText.gameObject.SetActive(false);
        audioSource.volume = DataManager.Instance.musicVolume;
        ComboCounter(0);
        LivesCount(0);
        CountPoints(0);
        InvokeRepeating("SpawnBalls", startDelay, repeatRate);
    }

    public void LivesCount(int live)
    {
        if (gameIsOn)
        {
            lives += live;
            livesText.text = "Lives: " + lives;
            if (lives == 0 )
            {
                GameOver();
            }
        }    
    }

    public void GameOver()
    {
        gameIsOn = false;
        gameOverText.gameObject.SetActive(true);
        restartBtn.gameObject.SetActive(true);
    }

    public void ChangePaused()
    {
        if (!isPaused)
        {
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
        else
        {
            pausePanel.gameObject.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }         
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
