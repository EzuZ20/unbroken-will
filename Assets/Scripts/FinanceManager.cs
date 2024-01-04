using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinanceManager : MonoBehaviour
{
    public int money = 5000; // Starting money
    public TextMeshProUGUI moneyText; // UI Text to display money
    public Button buyBreadButton; // Button to buy bread
    public TurnManager turnManager;
    public InventoryManager inventoryManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        UpdateMoneyUI();
        buyBreadButton.onClick.AddListener(BuyBread);
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = "Money: " + money + " UAH";
    }

    private void BuyBread()
    {
        int breadPrice = GetBreadPrice();
        if (money >= breadPrice)
        {
            money -= breadPrice;
            UpdateMoneyUI();
            inventoryManager.AddBread(1);
        }
        else
        {
            Debug.Log("Not enough money.");
        }
    }

    private int GetBreadPrice()
    {
        int currentDay = turnManager.GetCurrentDay();
        if (currentDay >= 3)
        {
            return 40;
        }
        else
        {
            return 20;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
