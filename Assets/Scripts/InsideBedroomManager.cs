using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InsideBedroomManager : MonoBehaviour
{


    private GraphicRaycaster raycaster; // UI Raycaster
    private PointerEventData pointerEventData; // Mouse click
    private EventSystem eventSystem; // Event System on UI

    public PlayerData playerData;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
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


                    if (result.gameObject.name == "Bed")
                    {
                        playerData.timeTokenCurrent = 0;

                        SceneManager.LoadScene("Homescene");
                    }

                   

                    


                }

            }

        }
    }
}
