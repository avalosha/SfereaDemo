using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTwoManager : MonoBehaviour
{
    public Card[] cards;
    public Sprite[] sprites;

    public List<int> reversedCards;
    private int score;
    private int pars;

    public GameObject gameOverView;
    public Animation animation;

    public Text scoreText;
    public Text timeText;

    private float duration;
    private float timer;
    private int seconds;
    private bool running;

    // Start is called before the first frame update
    void Start()
    {
        reversedCards = new List<int>();
        seconds = 0;
        duration = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (running && timer >= duration)
        {
            timer = 0f;
            seconds++;
            timeText.text = seconds.ToString() + " :Tiempo";
        }
    }

    void OnEnable() {
        Card.card += SetCards;
        if (!running) { running = true;}
    }

    void OnDisable() {
        Card.card -= SetCards;
    }

    private void SetCards(int number) {
        if (reversedCards.Count < 2) {
            reversedCards.Add(number);
            if(reversedCards.Count == 2) {
                StartCoroutine(ShowStates());
            }
        }
    }

    IEnumerator ShowStates()
    {
        yield return new WaitForSeconds(2);
        CheckCards();
    }

    private void CheckCards() {
        if (cards[reversedCards[0]].numberImage == cards[reversedCards[1]].numberImage){
            print("Good");
            cards[reversedCards[0]].DisableButton();
            cards[reversedCards[1]].DisableButton();
            score += 10;
            scoreText.text = "Puntuación: " + score.ToString();
            pars++;
            if (pars == 7) {
                running = false;
                GameOver();
            }
            print("Score: "+score);
        } else {
            print("Bad");
            cards[reversedCards[0]].TurnCard();
            cards[reversedCards[1]].TurnCard();
        }
        reversedCards.Clear();
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
