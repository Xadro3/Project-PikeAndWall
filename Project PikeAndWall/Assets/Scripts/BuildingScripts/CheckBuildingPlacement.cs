using UnityEngine;

public class CheckBuildingPlacement : MonoBehaviour
{
    BuildingManager buildingManager;
    ResourceManager resourceManager;
    ResourceAquisition resourceAquisition;

    [SerializeField] bool notEverywherePlaceable;
    [SerializeField] string placableOnTag;


    private int buildingCount; //nur ein Failsave, war erstmal zum �berpr�fen eines bugs da der nicht mehr auftreten sollte
    
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
        if (notEverywherePlaceable == true)
        {
            buildingManager.canPlace = false;
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
        if (other.gameObject.CompareTag("Buildings"))
        {
            buildingManager.canPlace = false;
            buildingCount++;
            Debug.Log(buildingCount);
        }

        if (other.gameObject.CompareTag(placableOnTag) && notEverywherePlaceable == true)
        {
            buildingManager.canPlace = true;


        }
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Buildings"))
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
