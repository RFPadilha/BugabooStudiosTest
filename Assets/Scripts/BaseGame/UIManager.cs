using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour, IDataPersistence
{
    public GameObject[] playerUIPanels;
    public GameObject[] joinMessages;

    public GameObject startMenuPanel;
    public GameObject pauseMenuPanel;

    public TextMeshProUGUI highScoreText;
    public int highscore = 0;

    public TextMeshProUGUI countdown;
    public TMP_InputField timeLimitText;
    float timeRemaining = 90f;
    public bool timerOn = false;
    bool gameEnded = false;
    bool saved = false;

    private void Start()
    {
        GameManager.instance.PlayerJoinedGame += PlayerJoinedGame;
        GameManager.instance.PlayerLeftGame += PlayerLeftGame;
        startMenuPanel.SetActive(true);
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 0f;
        timerOn = false;
        highScoreText.text = ($"High Score: {highscore}");//set highscore to saved score

    }
    private void Update()
    {

        if (gameEnded && !saved)
        {
            //loop through players scores to find highest one
            UpdateHighScore();

            //resets game
            startMenuPanel.SetActive(true);
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 0f;
            timerOn = false;
            highScoreText.text = ($"High Score: {highscore}");//set highscore to newly saved score
            saved = true;//guarantees this section runs only once
        }
        if (timerOn)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                UpdateTimer(Mathf.FloorToInt(timeRemaining));
            }
            else
            {
                timeRemaining = 0;
                timerOn = false;
                gameEnded = true;
            }
        }
    }
    public void LoadData(GameData data)
    {
        this.highscore = data.highScore;
        highScoreText.text = ($"High Score: {highscore}");//set highscore to saved score
    }
    public void SaveData(ref GameData data)
    {
        data.highScore = this.highscore;
    }

    void PlayerJoinedGame(PlayerInput playerInput)
    {
        ShowUIPanel(playerInput);
    }
    void PlayerLeftGame(PlayerInput playerInput)
    {
        HideUIPanel(playerInput);
    }
   //update high score fucntion, that loops through players active UI panels comparing scores
   void UpdateHighScore()
    {
        for (int i = 0; i < playerUIPanels.Length; i++)
        {
            if (playerUIPanels[i].activeSelf)
            {
                if(playerUIPanels[i].GetComponent<PlayerUIPanel>().localScore > highscore)
                {
                    highscore = playerUIPanels[i].GetComponent<PlayerUIPanel>().localScore;
                }
            }
        }
    }
    void ShowUIPanel(PlayerInput playerInput)
    {
        playerUIPanels[playerInput.playerIndex].SetActive(true);
        playerUIPanels[playerInput.playerIndex].GetComponent<PlayerUIPanel>().AssignPlayer(playerInput.playerIndex);
        joinMessages[playerInput.playerIndex].SetActive(false);

    }
    void HideUIPanel(PlayerInput playerInput)
    {
        playerUIPanels[playerInput.playerIndex].SetActive(false);
        joinMessages[playerInput.playerIndex].SetActive(true);
    }
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        timerOn = false;
    }
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        timerOn = true;
    }
    public void QuitGame()
    {
        Debug.Log("Application should have quit.");
        Application.Quit();
    }
    void UpdateTimer(float currentTime)
    {
        currentTime += 1;
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        countdown.text = ($"{minutes}:{seconds}");
    }
    public void StartGame()
    {
        gameEnded = false;
        saved = false;
        int.TryParse(timeLimitText.text, out int setTimer);
        timeRemaining = setTimer;
        Time.timeScale = 1f;
        timerOn = true;
        startMenuPanel.SetActive(false);
    }
}
