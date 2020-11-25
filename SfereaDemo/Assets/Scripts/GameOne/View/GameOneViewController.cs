using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOneViewController : MonoBehaviour
{

    public delegate void GameOneViewControllerDelegate();
    public static event GameOneViewControllerDelegate startGameOne;
    public static event GameOneViewControllerDelegate restartGameOne;

    public Text scoreText;
    public Text timeText;
    public GameObject startButton;

    public Animation anim;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        GameOneManager.updateScore += UpdateScore;
        GameOneManager.updateTime += UpdateTime;
        GameOneManager.gameOneOver += ShowGameOver;
    }

    private void OnDisable()
    {
        GameOneManager.updateScore -= UpdateScore;
        GameOneManager.updateTime -= UpdateTime;
        GameOneManager.gameOneOver -= ShowGameOver;
    }

    public void StartGame()
    {
        if (startGameOne != null)
        {
            startGameOne();
            startButton.SetActive(false);
        }
    }

    private void UpdateScore(int score)
    {
        scoreText.text = "Puntuación: " + score.ToString();
    }

    private void UpdateTime(int time)
    {
        timeText.text = time.ToString() + " :Tiempo";
    }

    private void ShowGameOver()
    {
        anim.Play("OpenLogin");
    }

    public void RestartGame()
    {
        anim.Play("CloseLogin");
        if (restartGameOne != null) { restartGameOne(); }
        UpdateScore(0);
        UpdateTime(0);
        if (!startButton.activeSelf) { startButton.SetActive(true); }
    }

    public void ReturnMainMenu()
    {
        anim.Play("CloseLogin");
        SceneManager.LoadSceneAsync(0);
    }

}
