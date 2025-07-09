using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeMenu : MonoBehaviour
{
    private GraphicRaycaster raycaster; // UI Raycaster
    private PointerEventData pointerEventData; // Mouse click
    private EventSystem eventSystem; // Event System on UI

    public PlayerData playerData;

    public GameObject inventoryScreen;

 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) // All mouse input
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                foreach (RaycastResult result in results)
                {
                    Debug.Log("UI Clicked: " + result.gameObject.name);


                    if (result.gameObject.name == "HuntButton") // Hunt Button
                    {
                        SceneManager.LoadScene("BattleScene");
                        
                    }

                    if (result.gameObject.name == "ExitButton") // Exit Button
                    {
                        Debug.Log("Quitting application...LOL...");
                        SceneManager.LoadScene("NewGameScene");
                        Application.Quit();
                    }

                    if (result.gameObject.name =="CookingPot") // Cooking Pot Healing
                    {
                        if (playerData.meat >= 5 && playerData.currentHealth < 20)
                        {

                            playerData.meat -= 5;
                            HomeAudioManager.instance.CookMeatOutside();
                            playerData.currentHealth = playerData.maxHealth;
                            Debug.Log("You cooked food and restored your health!");
                            Debug.Log($"Health: {playerData.currentHealth} / {playerData.maxHealth}");
                            Debug.Log($"Meat remaining: {playerData.meat}");
                        }
                        if (playerData.meat >= 5 && playerData.currentHealth == playerData.maxHealth)
                        {
                            Debug.Log("You are at max health!");
                        }
                        else
                        {
                            Debug.Log("You don't have enough meat! You need at least five.");
                        }

                    }

                    if (result.gameObject.name == "InventoryButton")
                    {
                        ShowInventory();
                    }

                    if (result.gameObject.name == "CloseInventoryButton")
                    {
                        CloseInventory();
                    }
                    


                   
                    
                }

            }

            else
            {
                Debug.Log("Clicked on empty space.");
            }
        }

        if (playerData.timeTokenCurrent >= 15)
        {
            SceneManager.LoadScene("InsideHomeScene");
        }
    }

         public void ShowInventory()
         {
            if (inventoryScreen != null)
             {
               inventoryScreen.SetActive(true);
             }

         }

        public void CloseInventory()
        {
           if (inventoryScreen != null)
           {
            inventoryScreen.SetActive(false);
            }
    }
}
