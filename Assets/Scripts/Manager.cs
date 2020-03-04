using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [Header("Unit References")]
    public UIScript canvas;
    public Player player;

    public static int existingBalls = 0;
    public static int maxBalls = 30;
    private static int playerHealth = 100;

    private static bool updateQueued = false;
    private static bool gameIsActive = false;

    // Start is called before the first frame update
    void Start()
    {

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
        if (playerHealth > 0)
        {
            playerHealth += amount;
        }
        updateQueued = true;
    }

    // Update all UI elements with new values
    public void UpdateUI()
    {
        if (updateQueued)
        {
            canvas.SetUIHealth(playerHealth);
            canvas.SetUIAmmo(maxBalls - existingBalls);
            updateQueued = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
