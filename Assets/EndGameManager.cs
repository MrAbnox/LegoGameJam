using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EndGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI endGameTimer;
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private float gameTime; //In seconds

    private float timer;
    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayAgain()
    {
        ResetGame();
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        timer = gameTime;
        endGameMenu.SetActive(false);
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);

        if (timer <= 0)
        {
            timer = 0;
            GameManager.instance.IsGamePlaying = false;
            endGameMenu.SetActive(true);
            endGameTimer.text = "0" + ":" + "00";
        }
        else
        {
            string additionalString = null;

            if (seconds < 10)
                additionalString = "0";

            endGameTimer.text = minutes.ToString() + ":" + additionalString + seconds.ToString();
        }

    }
}
