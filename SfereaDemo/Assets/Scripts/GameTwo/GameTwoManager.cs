using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTwoManager : MonoBehaviour
{
    public delegate void GameTwoManagerDelegate(int num);
    public static event GameTwoManagerDelegate updateScore;
    public static event GameTwoManagerDelegate updateTime;

    public delegate void GameTwoOver();
    public static event GameTwoOver gameTwoOver;

    public Card[] cards;
    public Sprite[] sprites;

    public List<int> reversedCards;
    private int score;
    private int pars;

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
            if (updateTime != null) { updateTime(seconds); }
        }
    }

    void OnEnable() {
        Card.card += SetCards;
        GameTwoViewController.startGameTwo += StartGame;
    }

    void OnDisable() {
        Card.card -= SetCards;
        GameTwoViewController.startGameTwo -= StartGame;
    }

    private void StartGame() {
        running = true;
        DeactivateCards(true);
    }

    private void SetCards(int number) {
        if (reversedCards.Count < 2) {
            reversedCards.Add(number);
            if(reversedCards.Count == 2) {
                DeactivateCards(false);
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
            //print("Good");
            cards[reversedCards[0]].DisableButton();
            cards[reversedCards[1]].DisableButton();
            score += 10;
            if (updateScore != null) { updateScore(score); }
            pars++;
            if (pars == 7) {
                running = false;
                if (gameTwoOver != null) { gameTwoOver(); }
            } else {
                DeactivateCards(true);
            }
            //print("Score: "+score);
        } else {
            //print("Bad");
            DeactivateCards(true);
            cards[reversedCards[0]].TurnCard();
            cards[reversedCards[1]].TurnCard();
        }
        reversedCards.Clear();
    }

    private void DeactivateCards(bool state) {
        for (int i = 0; i < 14; i++)
        {
            cards[i].Running = state;
        }
    }

}
