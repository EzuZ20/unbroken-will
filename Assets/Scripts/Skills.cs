using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Skills", menuName = "ScriptableObjects/Skills", order = 1)]
public class Skills : ScriptableObject
{
    public static Skills Instance { get; private set; }

    // Focus.
    public float focus = 50.0f; // Starting at 50%
    public delegate void FocusChanged(float newFocus);
    public event FocusChanged OnFocus;

    // Strength.
    public float strength = 50.0f; // Starting at 50%
    public delegate void StrengthChanged(float newStrength);
    public event StrengthChanged OnStrength;

    // Stamina.
    public float stamina = 50.0f; // Starting at 50%
    public delegate void StaminaChanged(float newStamina);
    public event StaminaChanged OnStamina;

    // Energy.
    public float energy = 100.0f; // Starting at 100%
    public delegate void EnergyChanged(float newEnergy);
    public event EnergyChanged OnEnergy;

    // Hunger.
    public float hunger = 100.0f; // Starting at 100%
    public delegate void HungerChanged(float newHunger);
    public event HungerChanged OnHunger;

    public void IncreaseFocus()
    {
        focus = Mathf.Min(focus + 5.0f, 100.0f); // Increase focus by 5%, up to maximum.
        OnFocus?.Invoke(focus);
    }

    public void IncreaseFocusMinus()
    {
        Debug.Log("Before IncreaseFocusMinus: " + focus);
        focus = Mathf.Min(focus + 3.0f, 100.0f); // Increase focus by 3%, up to maximum.
        Debug.Log("After IncreaseFocusMinus: " + focus);
        OnFocus?.Invoke(focus);
    }

    public void IncreaseStrength()
    {
        strength = Mathf.Min(strength + 5.0f, 100.0f); // Increase strength by 5%, up to maximum.
        OnStrength?.Invoke(strength);
    }

    public void IncreaseStamina()
    {
        stamina = Mathf.Min(stamina + 5.0f, 100.0f); // Increase stamina by 5%, up to maximum.
        OnStamina?.Invoke(stamina);
    }

    public void IncreaseStaminaPlus()
    {
        Debug.Log("Before IncreaseStaminaByTwo  : " + stamina);
        stamina = Mathf.Min(stamina + 7.0f, 100.0f); // Increase stamina by 7%, up to maximum.
        Debug.Log("After IncreaseStaminaByTwo: " + stamina);
        OnStamina?.Invoke(stamina);
    }

    public void ResetEnergy()
    {
        Debug.Log("Resetting energy");
        energy = 100.0f;
        OnEnergy?.Invoke(energy);
        Debug.Log("Energy after reset: " + energy);
    }

    public void ResetHunger()
    {
        hunger = 100.0f;
        OnHunger?.Invoke(hunger);
    }
    public void DecreaseFocus()
    {
        focus = Mathf.Max(focus - 2.0f, 0.0f); // Decrease focus by 2% per day, down to minimum.
        OnFocus?.Invoke(focus);
    }

    public void DecreaseStrength()
    {
        strength = Mathf.Max(strength - 2.0f, 0.0f); // Decrease strength by 2% per day, down to minimum.
        OnStrength?.Invoke(strength);
    }

    public void DecreaseStamina()
    {
        stamina = Mathf.Max(stamina - 2.0f, 0.0f); // Decrease stamina by 2% per day, down to minimum.
        OnStamina?.Invoke(stamina);
    }

    public void DecreaseEnergy(float amount)
    {
        energy = Mathf.Max(energy - amount, 0.0f);
        OnEnergy?.Invoke(energy);
    }

    public void DecreaseHunger(float amount)
    {
        hunger = Mathf.Max(hunger - amount, 0.0f);
        OnHunger?.Invoke(hunger);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
}
