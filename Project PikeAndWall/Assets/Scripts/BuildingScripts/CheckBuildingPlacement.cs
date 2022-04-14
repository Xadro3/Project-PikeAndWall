using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CheckBuildingPlacement : MonoBehaviour
{
    BuildingManager buildingManager;
    private int anzahlGebäude; //nur ein Failsave, war erstmal zum überprüfen eines bugs da der nicht mehr auftreten sollte
    
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Buildings"))
        {
            buildingManager.canPlace = false;
            anzahlGebäude++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Buildings"))
        {
            anzahlGebäude--;
            if(anzahlGebäude == 0)
            {
                buildingManager.canPlace = true;
            }

        }
    }
}
