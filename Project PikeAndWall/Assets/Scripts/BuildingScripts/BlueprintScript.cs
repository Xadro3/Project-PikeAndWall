using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueprintScript : MonoBehaviour
{

    public GameObject realBuilding;
    BuildingManager buildingManager;
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    void Update()
    {
        if (buildingManager.pendingBuilding == null && buildingManager.canPlace == true) //&& Input.GetMouseButton(0))
        {
            Instantiate(realBuilding,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}
