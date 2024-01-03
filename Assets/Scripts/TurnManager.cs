using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    private int currentDay = 0;
    private int currentTimeOfDay = 0;

    public string[] dailyTexts;
    public string[] timeOfDayTexts;
    public string[] morningNewsTexts;
    public string[] eveningNewsTexts;

    public TextMeshProUGUI dialogueBox;
    public TextMeshProUGUI timeOfDayText;
    public TextMeshProUGUI focusPercentageText;
    public TextMeshProUGUI strengthPercentageText;
    public TextMeshProUGUI staminaPercentageText;
    public TextMeshProUGUI newsDialogueBox;
    public TextMeshProUGUI energyPercentageText;

    public Skills playerSkills;

    public Button sleepButton;
    public Button readButton;
    public Button exerciseButton;
    public Button jogButton;
    public Button readNewsButton;

    public DayCycleManager dayCycleManager;


    public void AdvanceDay()
    {
        currentDay++;
        // Logic to happen each day should be here.
        if (currentDay < dailyTexts.Length)
        {
            dialogueBox.text = dailyTexts[currentDay];
        }
        else
        {
            dialogueBox.text = "Work in progress.";
        }
        currentTimeOfDay = 0;
        timeOfDayText.text = timeOfDayTexts[currentTimeOfDay];
        dayCycleManager.UpdateLighting(currentTimeOfDay);
        sleepButton.interactable = false;
        playerSkills.DecreaseFocus();
        playerSkills.DecreaseStrength();
        playerSkills.DecreaseStamina();
        UpdateFocusUI(playerSkills.focus);
        UpdateStrengthUI(playerSkills.strength);
        UpdateStaminaUI(playerSkills.stamina);
        UpdateEnergyUI(playerSkills.energy);

        jogButton.interactable = true;
        exerciseButton.interactable = true;
        readButton.interactable = true;

        Debug.Log("Day advanced to: " + currentDay);
    }

    public void AdvanceTimeOfDay()
    {
        currentTimeOfDay++;

        // Check if currentTimeOfDay has exceeded the bounds of the array
        if (currentTimeOfDay >= timeOfDayTexts.Length)
        {
            //currentTimeOfDay = 0;
            AdvanceDay();
        }
        else
        {
            timeOfDayText.text = timeOfDayTexts[currentTimeOfDay];
            dayCycleManager.UpdateLighting(currentTimeOfDay);
        }

        sleepButton.interactable = (currentTimeOfDay == timeOfDayTexts.Length - 1);

        // Disable buttons if it's evening.
        if (currentTimeOfDay == 2)
        {
            jogButton.interactable = false;
            exerciseButton.interactable = false;
            readButton.interactable = false;
            
        }

    }

    public void Sleep()
    {
        // If it's "Afternoon", reset the time of day to "Early Morning"
        if (currentTimeOfDay == 1)
        {
            currentTimeOfDay = 0;
            timeOfDayText.text = timeOfDayTexts[currentTimeOfDay];
            dayCycleManager.UpdateLighting(currentTimeOfDay);
        }
        playerSkills.ResetEnergy();
        UpdateEnergyUI(playerSkills.energy);
    }

    public void ReadBook()
    {
        if (playerSkills.energy > 0)
        {
            playerSkills.IncreaseFocus();
            playerSkills.DecreaseEnergy(10); // Current energy cost for reading - 10 units.
            UpdateFocusUI(playerSkills.focus);
            UpdateEnergyUI(playerSkills.energy);
            AdvanceTimeOfDay(); // Move to the next time of day when "Read" is clicked.
        }
        
    }

    public void Exercise()
    {
        if (playerSkills.energy > 0)
        {
            playerSkills.IncreaseStrength();
            playerSkills.IncreaseStamina();
            playerSkills.DecreaseEnergy(25); // Current energy cost for exercising - 25 units.
            UpdateStrengthUI(playerSkills.strength);
            UpdateStaminaUI(playerSkills.stamina);
            UpdateEnergyUI(playerSkills.energy);
            AdvanceTimeOfDay();
        }    
        
    }

    public void Jog()
    {
        if (playerSkills.energy > 0)
        {
            Debug.Log("Jogging - Increasing Stamina and Focus");
            playerSkills.IncreaseStaminaPlus();
            playerSkills.IncreaseFocusMinus();
            playerSkills.DecreaseEnergy(20); // Current energy cost for jogging - 20 units.
            UpdateStaminaUI(playerSkills.stamina);
            UpdateFocusUI(playerSkills.focus);
            UpdateEnergyUI(playerSkills.energy);
            AdvanceTimeOfDay();
        }
        
    }

    private void UpdateFocusUI(float newFocus)
    {
        float progress = newFocus / 100.0f;
        focusPercentageText.text = "Focus: " + newFocus.ToString("F0") + "%";

    }

    private void UpdateStrengthUI(float newStrength)
    {
        float progress = newStrength / 100.0f;
        strengthPercentageText.text = "Strength: " + newStrength.ToString("F0") + "%";
    }

    private void UpdateStaminaUI(float newStamina)
    {
        float progress = newStamina / 100.0f;
        staminaPercentageText.text = "Stamina: " + newStamina.ToString("F0") + "%";
    }

    private void UpdateEnergyUI(float newEnergy)
    {
        float progress = newEnergy / 100.0f;
        energyPercentageText.text = "Energy: " + newEnergy.ToString("F0") + "%";
    }

    public void ReadNews()
    {
        string newsText = "";
        if (currentTimeOfDay == 0)
        {
            newsText = morningNewsTexts[currentDay];
        }
        else if (currentTimeOfDay == 2)
        {
            newsText = eveningNewsTexts[currentDay];
        }
        newsDialogueBox.text = newsText;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the sleep button to non-interactable if it's not evening
        sleepButton.interactable = (currentTimeOfDay == timeOfDayTexts.Length - 1);
        playerSkills = ScriptableObject.CreateInstance<Skills>();
        //playerSkills = Skills.Instance;
        playerSkills.OnFocus += UpdateFocusUI;
        playerSkills.OnStrength += UpdateStrengthUI;
        playerSkills.OnStamina += UpdateStaminaUI;
        playerSkills.OnEnergy += UpdateEnergyUI;
        UpdateFocusUI(playerSkills.focus);
        UpdateStrengthUI(playerSkills.strength);
        UpdateStaminaUI(playerSkills.stamina);
        UpdateEnergyUI(playerSkills.energy);
        readNewsButton.onClick.AddListener(ReadNews);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        playerSkills.OnFocus += UpdateFocusUI;
        playerSkills.OnStrength += UpdateStrengthUI;
        playerSkills.OnStamina += UpdateStaminaUI;
        playerSkills.OnEnergy += UpdateEnergyUI;
    }

   
}
