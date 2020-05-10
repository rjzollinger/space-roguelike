using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [Header("Unit References")]
    public UIScript canvas;
    public Transform dynamicCanvas;
    public Player player;

    public readonly int roomsNeeded = 8;
    private int roomsCompleted = 0; 

    public static int existingBalls = 0;
    public static int maxBalls = 30;
    private static int playerHealth = 100;
    private static int maxPlayerHealth = 100;

    private static bool updateQueued = false;
    private static bool gameIsActive = false;

    private LevelLoader LevelLoader;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<UIScript>();
        dynamicCanvas = GameObject.Find("DynamicCanvas").GetComponent<Transform>();
        LevelLoader = canvas.GetComponent<LevelLoader>();
    }

    public int GetRoomsCompleted() {
        return roomsCompleted;
    }

    public void IncrementRoomsCompleted() {
        roomsCompleted++;
    }

    public static void ToggleGameActiveStatus()
    {
        gameIsActive = !gameIsActive;
    }

    public static bool GetGameActiveStatus()
    {
        return gameIsActive;
    }

    // Update the existing ball count by amount
    public static void UpdateBallCount(int amount)
    {
        existingBalls = Mathf.Clamp(existingBalls + amount, 0, maxBalls);
        updateQueued = true;
    }

    // Update the player health by amount
    public static void UpdateHealth(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, maxPlayerHealth);
        updateQueued = true;
    }

    public static void ResetPlayer()
    {
        UpdateHealth(maxPlayerHealth);
        UpdateBallCount(maxBalls);
    }

    // Update all UI elements with new values
    public void UpdateUI()
    {
        if (updateQueued)
        {
            canvas.SetUIHealth(playerHealth, maxPlayerHealth);
            canvas.SetUIAmmo(maxBalls - existingBalls, maxBalls);
            updateQueued = false;
        }
    }

    public void CheckDeath()
    {
        if (playerHealth <= 0 && gameIsActive) {
            dynamicCanvas.gameObject.SetActive(false);
            canvas.ShowDeathScreen();
        }
    }

    public void WinGame()
    {
        canvas.ShowWinScreen();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        //Debug.Log(GetGameActiveStatus());
        CheckDeath();
    }

    public void NextRoom()
    {
        LevelLoader.LoadNextLevel(Random.Range(1,10));
    }
}
