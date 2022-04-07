using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyBuilding : MonoBehaviour
{

    [SerializeField] GameObject destroyEffekt;
    [SerializeField] float destroyEffektTime;
    BuildingManager buildingManager;
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnMouseDown()
    {
        if (buildingManager.destroyMode == true)
        {
            DestroyBuildingEffekt();
        }
    }

    private void DestroyBuildingEffekt()
    {
        if (destroyEffekt != null)
        {
            Destroy(Instantiate(destroyEffekt, transform.position, transform.rotation), destroyEffektTime);

        }
        Destroy(gameObject);
    }

}
