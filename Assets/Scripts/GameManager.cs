using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public BulletSpawn bulletSpawnPlayer1;
    public BulletSpawn bulletSpawnPlayer2;
    public TMP_Text endGameText;
    public TMP_Text endGameTextShadow;
    public GameObject endGamePanel;

    void Start()
    {
        endGameText.text = "";
        endGameTextShadow.text = "";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Update()
    {
        if (bulletSpawnPlayer1.gameOver && bulletSpawnPlayer2.gameOver)
        {
            endGamePanel.SetActive(true);
            endGameText.text = "Draw!";
            endGameTextShadow.text = "Draw!";
        }
        else if (bulletSpawnPlayer1.gameOver)
        {
            endGamePanel.SetActive(true);
            endGameText.text = "Player 2 Wins!";
            endGameTextShadow.text = "Player 2 Wins!";
        }
        else if (bulletSpawnPlayer2.gameOver)
        {
            endGamePanel.SetActive(true);
            endGameText.text = "Player 1 Wins!";
            endGameTextShadow.text = "Player 1 Wins!";
        }
    }
}
