using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private static double attackSpeed;
    [SerializeField] private static double criticalRate;
    [SerializeField] private static double movementSpeed;
    [SerializeField] private static double attackDamage;
    [SerializeField] private static double currencies;
    [SerializeField] private static double playerHealth;
    [SerializeField] private static double playerStamina;
    private static TextMeshProUGUI[] uiTexts;
    private static Slider[] playerSliders;
    // Start is called before the first frame update
    void Awake()
    {
        currencies = 0;
        uiTexts = new TextMeshProUGUI[4];
        uiTexts[0] = Utility.FindUIObjectWithName("CurrencyText").GetComponent<TextMeshProUGUI>();
        uiTexts[0].text = currencies.ToString();
        uiTexts[1] = Utility.FindUIObjectWithName("PlayerHealth").GetComponentInChildren<TextMeshProUGUI>();
        uiTexts[1].text = playerHealth.ToString();
        uiTexts[2] = Utility.FindUIObjectWithName("PlayerStamina").GetComponentInChildren<TextMeshProUGUI>();
        uiTexts[2].text = playerStamina.ToString();
        playerSliders = new Slider[2];
        playerSliders[0] = Utility.setPlayerSliderUI("PlayerHealth", 400);
        playerSliders[1] = Utility.setPlayerSliderUI("PlayerStamina", 100);
        playerHealth = playerSliders[0].value;
        playerStamina = playerSliders[1].value;
    }

    // Update is called once per frame
    void Update()
    {
        uiTexts[0].text = currencies.ToString("0.##");
        playerSliders[0].value = (float)playerHealth;
        playerSliders[1].value = (float)playerStamina;
        uiTexts[1].text = playerHealth.ToString("0");
        uiTexts[2].text = playerStamina.ToString("0");
    }

    public static void AddCurency(double amount)
    {
        currencies += amount;
    }

    public static void receiveDamage(double amount)
    {
        if (playerHealth > 0f)
        {
            playerHealth -= amount;
        }
    }
}
