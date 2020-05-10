using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    #region Public UI References

    [Header("Main Menu Elements")]
    public GameObject mainMenuPanel;
    [Space(10)]
    public Button playButton;
    public Button instructionsButton;
    public Button creditsButton;
    public Button quitButton;
    [Space(10)]
    public Button instructionsPanel;
    public Button creditsPanel;
    public Button endPanel;
    
    [Header("Health Elements")]
    public Text healthText;
    public Slider healthSlider;

    [Header("Score Elements")]
    public Text scoreText;

    [Header("Time Elements")]
    public Text timeText;

    [Header("Ammo Elements")]
    public Text ammoText;
    public Slider ammoSlider;

    [Header("End Panel Elements")]
    public Text endTitle;
    public Text endText;

    #endregion

    #region UI State Variables

    // Tracks if the timer is currently active
    private bool timerIsActive = false;
    // Tracks if the timer is counting up or down
    private bool timerCountUp = true;
    // Internal variable for controlling timing
    private float internalSecondTracker = 0;
    // Current time to display in seconds
    private int seconds = 0;
    
    #endregion

    #region UI Value Setters

    // Set the UI health bar to health
    public void SetUIHealth(int health, int maxHealth)
    {
        healthText.text = health.ToString();
        healthSlider.value = (float)health;
        healthSlider.maxValue = (float)maxHealth;
    }

    // Set the UI score to score
    public void SetUIScore(int score)
    {
        scoreText.text = score.ToString();
    }

    // Set the UI ammo to ammo
    public void SetUIAmmo(int ammo, int maxAmmo)
    {
        ammoText.text = string.Format("{0:D2} / {1:D2}", ammo, maxAmmo);
        ammoSlider.value = (float)ammo;
        ammoSlider.maxValue = (float)maxAmmo;
    }

    #endregion

    #region Time Controls

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

    private void onPlayClick()
    {
        // Temporary; update to load scene later
        Manager.ResetPlayer();
        mainMenuPanel.SetActive(false);
        Manager.ToggleGameActiveStatus();
        Time.timeScale = 1;
        StartTimer(true);
    }

    private void onInstructionsClick()
    {
        instructionsPanel.gameObject.SetActive(true);
    }

    private void onCreditsClick()
    {
        creditsPanel.gameObject.SetActive(true);
    }

    private void onQuitClick()
    {
        Debug.Log("Quit");
    }

    private void onCreditsPanelClick()
    {
        creditsPanel.gameObject.SetActive(false);
    }

    private void onInstructionsPanelClick()
    {
        instructionsPanel.gameObject.SetActive(false);
    }

    private void onEndPanelClick()
    {
        endPanel.gameObject.SetActive(false);
        SceneManager.LoadScene(0);
        DeleteAll();
    }

    #endregion

    private void DeleteAll()
    {
         foreach (GameObject o in Resources.FindObjectsOfTypeAll<GameObject>()) {
             if (o.GetComponent<DontDestroyOnLoad>() != null)
             {
                 Destroy(o);
             }
         }
     }

    // Show the game over screen
    public void ShowDeathScreen()
    {
        Time.timeScale = 0;
        mainMenuPanel.SetActive(true);
        endPanel.gameObject.SetActive(true);
        endTitle.text = "Defeat";
        endText.text = "You failed to escape the ruins...\n\nYou survived for:\n" + string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
        Manager.ToggleGameActiveStatus();
    }

    // Show the victory screen
    public void ShowWinScreen()
    {
        Time.timeScale = 0;
        mainMenuPanel.SetActive(true);
        endPanel.gameObject.SetActive(true);
        endTitle.text = "Success";
        endText.text = "You have successfully escaped from the ruins\n\nIt took you:\n" + string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
        Manager.ToggleGameActiveStatus();
    }

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(onPlayClick);
        instructionsButton.onClick.AddListener(onInstructionsClick);
        creditsButton.onClick.AddListener(onCreditsClick);
        quitButton.onClick.AddListener(onQuitClick);

        creditsPanel.onClick.AddListener(onCreditsPanelClick);
        instructionsPanel.onClick.AddListener(onInstructionsPanelClick);
        endPanel.onClick.AddListener(onEndPanelClick);
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime(timerIsActive, timerCountUp);

        if (Input.GetKeyDown("u"))
        {
            SetUIHealth(Random.Range(0,101), 100);
            SetUIAmmo(Random.Range(0,61), 60);
        }
    }
}