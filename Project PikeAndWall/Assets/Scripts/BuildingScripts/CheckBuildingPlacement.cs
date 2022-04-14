using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckBuildingPlacement : MonoBehaviour
{
    BuildingManager buildingManager;
    private int anzahlGeb�ude; //nur ein Failsave, war erstmal zum �berpr�fen eines bugs da der nicht mehr auftreten sollte
    
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Buildings"))
        {
            buildingManager.canPlace = false;
            anzahlGeb�ude++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Buildings"))
        {
            anzahlGeb�ude--;
            if(anzahlGeb�ude == 0)
            {
                buildingManager.canPlace = true;
            }

        }
    }
}
