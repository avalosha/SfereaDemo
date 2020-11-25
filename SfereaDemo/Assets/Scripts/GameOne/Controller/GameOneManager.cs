using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOneManager : MonoBehaviour
{
    public delegate void GameOneManagerDelegate(int num);
    public static event GameOneManagerDelegate updateScore;
    public static event GameOneManagerDelegate updateTime;

    public delegate void GameOneOver();
    public static event GameOneOver gameOneOver;

    private List<List<int>> states;
    private List<int> selectedButtons;
    private bool state;
    private int index;
    private int score;
    private bool running;
    private float duration;
    private float timer;
    private int seconds;
    public CustomButton[] buttons;
    

    // Start is called before the first frame update
    void Start()
    {
        states = new List<List<int>>();
        selectedButtons = new List<int>();
        state = true;
        running = false;
        seconds = 0;
        duration = 1f;
        score = 0;
        index = 0;

        for (int i = 0; i < 10; i++)
        {
            List<int> numbers = new List<int>();
            for (int j = 0; j <= i; j++)
            {
                numbers.Add(Random.Range(0, 4));
            }
            states.Add(numbers);
        }

    }

    private void OnEnable()
    {
        GameOneViewController.startGameOne += StartGame;
        GameOneViewController.restartGameOne += Start;
        CustomButton.customButton += SelectedButtons;
    }

    private void OnDisable()
    {
        GameOneViewController.startGameOne -= StartGame;
        GameOneViewController.restartGameOne -= Start;
        CustomButton.customButton -= SelectedButtons;
    }

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

    public void StartGame()
    {
        running = true;
        StartCoroutine(ShowStates());
    }

    IEnumerator ShowStates()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < states[index].Count; i++)
        {
            yield return new WaitForSeconds(1);
            buttons[states[index][i]].TurnWhiteColor();
            yield return new WaitForSeconds(1);
            buttons[states[index][i]].TurnOriginalColor();
        }
        yield return new WaitForSeconds(1);
        state = false;
    }

    private void SelectedButtons(int num)
    {
        if (state) { return; }
        if (selectedButtons.Count < states[index].Count)
        {
            //print("Presiono: " + num);
            selectedButtons.Add(num);
            if (selectedButtons.Count == states[index].Count)
            {
                running = false;
                state = true;
                if (CompareLists())
                {
                    print("Good");
                    score += 10 * states[index].Count;
                    if (updateScore != null) { updateScore(score); }
                    selectedButtons.Clear();
                    index++;
                    if (index < 10)
                    {
                        StartGame();
                    } else
                    {
                        if (gameOneOver != null) { gameOneOver(); }
                    }
                }
                else
                {
                    print("Bad");
                    if (gameOneOver != null) { gameOneOver(); }
                }
            }
        }
        
    }

    private bool CompareLists()
    {
        for (int i = 0; i < states[index].Count; i++)
        {
            if (states[index][i] != selectedButtons[i])
            {
                return false;
            }
        }
        return true;
    }
}
