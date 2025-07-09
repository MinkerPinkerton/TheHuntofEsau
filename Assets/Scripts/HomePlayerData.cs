using UnityEngine;

public class HomePlayerData : MonoBehaviour
{

    public PlayerData playerData;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log($"Welcome back, {playerData.playerName}!");
        Debug.Log($"Health: {playerData.currentHealth}/{playerData.maxHealth}");
        Debug.Log($"XP: {playerData.experience}");
        Debug.Log($"Meat: {playerData.meat}");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
