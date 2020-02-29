using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    #region Public UI References

    [Header("Main Menu Elements")]
    public Button playButtton;
    public Button instructionsButton;
    public Button creditsButton;
    public Button quitButton;

    [Header("Heath Elements")]
    public Text healthText;
    public Slider healthSlider;

    [Header("Score Elements")]
    public Text scoreText;

    [Header("Time Elements")]
    public Text timeText;

    #endregion

    #region UI State Variables

    private bool timerIsActive = false;
    private bool timerCountUp = true;
    private float internalSecondTracker = 0;
    private int seconds = 0;

    // Debugging variables
    private int health = 100;
    
    #endregion

    #region UI Value Setters

    // Set the UI health bar to health
    public void SetUIHealth(int health)
    {
        healthText.text = health.ToString();
        healthSlider.value = (float)health;
    }

    // Set the UI score to score
    public void SetScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // Set the UI time to a minutes and seconds
    public void SetTime(int seconds)
    {
        this.seconds = seconds;
        timeText.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }

    // Start timer, either counting up or down
    public void StartTimer(bool countUp)
    {
        timerIsActive = true;
        timerCountUp = countUp;
        internalSecondTracker = 0;
    }

    // Stop timer without resetting time
    public void StopTimer()
    {
        timerIsActive = false;
        internalSecondTracker = 0;
    }

    // Updates the time based on active status and count direction
    private void UpdateTime(bool timerIsActive, bool timerCountUp)
    {
        if (timerIsActive)
        {
            internalSecondTracker += Time.deltaTime;
            if (seconds >= 0 && internalSecondTracker >= 1)
            {
                SetTime(this.seconds + (timerCountUp ? 1 : -1));
                internalSecondTracker = 0;
            }
        }
    }

    #endregion

    #region Main Menu Button Callbacks

    public void onPlayClick()
    {
        Debug.Log("Play");
    }

    public void onInstructionsClick()
    {
        Debug.Log("Instructions");
    }

    public void onCreditsClick()
    {
        Debug.Log("Credits");
    }

    public void onQuitClick()
    {
        Debug.Log("Quit");
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playButtton.onClick.AddListener(onPlayClick);
        instructionsButton.onClick.AddListener(onInstructionsClick);
        creditsButton.onClick.AddListener(onCreditsClick);
        quitButton.onClick.AddListener(onQuitClick);

        StartTimer(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            health--;
            SetUIHealth(health);
            SetScore(health);
        }

        UpdateTime(timerIsActive, timerCountUp);
    }
}