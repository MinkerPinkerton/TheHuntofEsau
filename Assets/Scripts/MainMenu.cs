using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private GraphicRaycaster raycaster; // UI Raycaster
    private PointerEventData pointerEventData; // Mouse click
    private EventSystem eventSystem; // Event System on UI





    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;

        
    }

    // Update is called once per frame
    void Update()
    {

        // Able to click on UI
        if (Input.GetMouseButtonDown(0))
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


                    if (result.gameObject.name == "HuntButton")
                    {
                        SceneManager.LoadScene("HomeScene");
                    }

                    if (result.gameObject.name == "ExitButton")
                    {
                        Debug.Log("Quitting application...LOL...");
                        Application.Quit();
                    }
                }
                
            }

            else
            {
                Debug.Log("Clicked on empty space.");
            }
        }

    }
}
