using UnityEngine;

public class BlueprintScript : MonoBehaviour
{

    public GameObject realBuilding;
    CheckBuildingPlacement check;
    BuildingManager buildingManager;
    ResourceManager resource;
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        check = gameObject.GetComponent<CheckBuildingPlacement>();
        resource = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();

    }

    void Update()
    {
        if (buildingManager.pendingBuilding == null && buildingManager.canPlace == true)
        {
            resource.SpendResource(check.resourceCost);
            Instantiate(realBuilding, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
