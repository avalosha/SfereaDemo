using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTwoViewController : MonoBehaviour
{
    public delegate void GameTwoViewControllerDelegate();
    public static event GameTwoViewControllerDelegate startGameTwo;

    public GameObject gameOverView;
    public Animation animation;

    public Text scoreText;
    public Text timeText;

    public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        GameTwoManager.updateScore += UpdateScore;
        GameTwoManager.updateTime += UpdateTime;
        GameTwoManager.gameTwoOver += GameOver;
    }

    private void OnDisable()
    {
        GameTwoManager.updateScore -= UpdateScore;
        GameTwoManager.updateTime -= UpdateTime;
        GameTwoManager.gameTwoOver -= GameOver;
    }

    public void StartGame()
    {
        if (startGameTwo != null)
        {
            startGameTwo();
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

    private void GameOver() {
        gameOverView.SetActive(true);
        animation.Play("OpenLogin");
    }

    public void ReturnMenu(){
        animation.Play("CloseLogin");
        SceneManager.LoadSceneAsync(0);
    }
}
