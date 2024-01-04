using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public int bread = 2; // Starting bread
    public TextMeshProUGUI breadText; // UI Text to display bread
    public FinanceManager financeManager;
    public Skills playerSkills;
    public Button eatBreadButton;
    
    // Start is called before the first frame update
    private void Start()
    {
        UpdateBreadUI();
    }

    public void AddBread(int amount)
    {
        bread += amount; // Add the specified amount of bread;
        UpdateBreadUI();
    }

    public void EatBread()
    {
        if (bread > 0)
        {
            bread--;
            playerSkills.ResetHunger();
            UpdateBreadUI();
        }
        else
        {
            Debug.Log("No bread available.");
        }
        
    }
    private void UpdateBreadUI()
    {
        breadText.text = "Bread: " + bread;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
