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

    #endregion

    // Debugging variables
    private int health = 100;

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
    }
}
