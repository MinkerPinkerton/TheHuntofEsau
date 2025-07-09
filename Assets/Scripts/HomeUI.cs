using UnityEngine;
using TMPro;


public class HomeUI : MonoBehaviour
{
    public PlayerData playerData;

    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerManaText;
    public TextMeshProUGUI playerMeatText;
    public TextMeshProUGUI playerXPText;







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayHealth();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayHealth();
    }

    void DisplayHealth()
    {
        playerHealthText.text = $"{playerData.currentHealth} / {playerData.maxHealth}";
        playerManaText.text = $"{playerData.currentMana} / {playerData.maxMana}";
        playerMeatText.text = $"{playerData.meat}";
        playerXPText.text = $"{playerData.experience}";
    }

    
}
