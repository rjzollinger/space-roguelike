using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [Header("Heath Elements")]
    public Text healthText;
    public Slider healthSlider;
    private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Set the UI health bar to health
    public void SetUIHealth(int health)
    {
        healthText.text = health.ToString();
        healthSlider.value = (float)health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            health--;
            SetUIHealth(health);
        }
    }
}
