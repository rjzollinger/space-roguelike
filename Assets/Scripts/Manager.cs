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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public static void UpdateHealth(int update) {
        if (playerHealth > 0) {
            playerHealth += update;
        }
    }

    public void UpdateUI() {
        canvas.SetUIHealth(playerHealth);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }
}
