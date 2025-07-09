using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    // Esau Stats
    // Health
    public string playerName = "Esau";
    public int maxHealth = 20;
    public int currentHealth = 20;
    public int minHealth = 0;

    // Mana
    public int maxMana = 10;
    public int currentMana = 0;
    public int minMana = 0;
    
    // Attack
    public int attackBasicDamage = 5;
    [Range(0f, 1f)] public float hitChance = 0.8f;


    // Game Loop
    public int experience = 0;
    public int meat = 0;


    // Time passing
    public int timeTokenCurrent = 0;
    public int timeTokenBaseline = 0;
    public int timeTokenMaximum = 15;

    public int timeDayTokenCurrent = 0;
    public int timeDayTokenBaseline = 0;
    public int timeDayTokenMaximum = 10;

    // Quests
    public bool swordInStoneQuest = false;
    public bool manaQuest = false;
    public bool edenQuest = false;



    // Esau Items
    public bool equippedSpear = false;


    

    public void ResetStats()
    {
        currentHealth = maxHealth;
        attackBasicDamage = 5;
        experience = 0;
        meat = 0;

        timeTokenCurrent = timeTokenBaseline;
        swordInStoneQuest = false;
        equippedSpear = false;
        Debug.Log("Stats reset!");
    }

   
    public void SwordInStoneQuest()
    {
        swordInStoneQuest = true;
        attackBasicDamage += 10;
    }

    public void ManaQuestTrigger()
    {
        manaQuest = true;
        currentMana = 10;
    }

    public void GatesOfEdenTrigger()
    {
        edenQuest = true;
    }
    
    public void EquipSpear()
    {
        equippedSpear = true;
        attackBasicDamage += 5;
    }
}
