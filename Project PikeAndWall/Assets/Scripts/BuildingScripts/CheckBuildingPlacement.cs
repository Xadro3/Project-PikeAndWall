using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBuildingPlacement : MonoBehaviour
{
    BuildingManager buildingManager;
    ResourceManager resourceManager;
    //ResourceAquisition resourceAquisition;

    [SerializeField] bool notEverywherePlaceable;
    [SerializeField] string placableOnTag;
    public List<ResourceValue> resourceCost;
    public int wood;
    public int gold;
    public int iron;
    public int stone;
    public bool enoughResources;
    private int buildingCount; //nur ein Failsave, war erstmal zum �berpr�fen eines bugs da der nicht mehr auftreten sollte
    
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        if (notEverywherePlaceable == true)
        {
            buildingManager.canPlace = false;
        }
        enoughResources = false;
        InvokeRepeating("CheckResources", 0, 1.0f);
    }
    
    private void CheckResources()
    {
        resourceManager.resourceDictionary.TryGetValue(ResourceType.Wood,out wood);
        resourceManager.resourceDictionary.TryGetValue(ResourceType.Stone, out stone);
        resourceManager.resourceDictionary.TryGetValue(ResourceType.Iron, out iron);
        resourceManager.resourceDictionary.TryGetValue(ResourceType.Gold, out gold);

        foreach (ResourceValue resource in resourceCost)
        {
            if (!resourceManager.CheckResourceAvailability(resource))
            {
                enoughResources = false;
                break;
            }
            else
            {
                enoughResources = true;
            }
        }

        
        
    }

    //private void NotEnoughResource()
    //{
    //    if (resourceManager.ResourceValue < resourceAquisition.buildCost)
    //    {
    //        buildingManager.canPlace = false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            buildingManager.canPlace = false;
            buildingCount++;
            Debug.Log(buildingCount);
        }

        if (other.gameObject.CompareTag(placableOnTag) && notEverywherePlaceable == true && enoughResources )
        {
            buildingManager.canPlace = true;


        }
    }

   
    //private void OnTriggerStay(Collider other
    //{
        
    //}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Building"))
        {
            buildingCount--;
            Debug.Log(buildingCount);
            if (buildingCount == 0 && notEverywherePlaceable == false)
            {
                buildingManager.canPlace = true;
            }

        }
        if (other.gameObject.CompareTag(placableOnTag) && notEverywherePlaceable == true)
        {
            buildingManager.canPlace = false;
        }
    }
}
