using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public int raycastLength;

    public GameObject[] blueprint;
    public GameObject pendingBuilding;

    private Vector3 position;

    private RaycastHit hit;

    public float rotateAmount;

    public float gridSize;
    bool gridOn;

    public bool canPlace;
    public bool destroyMode;


    [SerializeField] private Material[] materials;

    [SerializeField] private LayerMask layerMask;

    [SerializeField] ResourceManager resourceManager;

    ResourceAquisition resourceAquisition;

    DestroyBuilding destroyBuilding;

    //private void Start()
    //{
    //    blueprint[index].GetComponent<ResourceAquisition>().buildCost;
    //}

    void Update()
    {
        if (pendingBuilding != null)
        {

            if (gridOn)
            {
                pendingBuilding.transform.position = new Vector3(
                    RoundToNearestGrid(position.x),
                    RoundToNearestGrid(position.y),
                    RoundToNearestGrid(position.z)
                    );
            }

            else
            {
                pendingBuilding.transform.position = position;
            }

            if (Input.GetMouseButtonDown(0) && canPlace && !IsPointerOverUI())
            {
                PlaceBuilding();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Destroy(pendingBuilding);
                pendingBuilding = null;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                RotateBuilding();
            }

            UpdateMaterials();

        }
    }

    public void PlaceBuilding()
    {
        pendingBuilding = null;
    }

    public void RotateBuilding()
    {
        pendingBuilding.transform.Rotate(Vector3.up, rotateAmount);
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, raycastLength, layerMask))
        {
            position = hit.point;
        }
    }

    void UpdateMaterials()
    {
        if (canPlace)
        {
            pendingBuilding.GetComponent<MeshRenderer>().material = materials[0];
        }

        if (!canPlace)
        {
            pendingBuilding.GetComponent<MeshRenderer>().material = materials[1];
        }
    }


    public void SelectBuilding(int index)
    {
     
        if (pendingBuilding != null)
        {
            Destroy(pendingBuilding);
        }

        pendingBuilding = Instantiate(blueprint[index], position, transform.rotation);

        //So wars im Vid aufgebaut, dazu fehlen mir aber die elemente, also muss ich nen workaround suchen oder die elemente sinnvoll nachkreiren:

        //foreach (UIBuildSelectionHandler buildItem in buildOptions)
        //{
        //    if (buildItem.BuildData == null)
        //    {
        //        buildItem.ToggleActive(false);
        //        continue;
        //    }

        //    buildItem.ToggleActive(true);
        //    foreach (ResourceValue item in resourceAquisition.buildCost)
        //    {
        //        if (resourceManager.CheckResourceAvailability(item) == false)
        //        {
        //            buildItem.ToggleActive(false);
        //            break;
        //        }
        //    }
        //}

        //if (resourceManager.resourceRequired < resourceAquisition.buildCost)
        //{
        //    Debug.Log("NoBuildingAllowed");
        //}
        //_requiredResource = blueprint[index].GetComponent<ResourceAquisition>().buildCost;
        //resourceManager.CheckResourceAvailability(ResourceValue);


        //if (resourceManager.CheckResourceAvailability(ResourceValue buildCost)
        //{

        //}

    }


    public void ToggleGrid(bool gridToggle) //ein grid kann mit diesem Toggle aktiviert und deaktiviert werden
    {
        if (gridToggle == true)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    public void ToggleDestoryBuild(bool destroyToggle) //Geb�ude sind zerst�rbar wenn dieser Toggle Aktiv ist
    {
        if (destroyToggle == true)
        {
            destroyMode = true;
        }
        else
        {
            destroyMode = false;
        }
    }

    float RoundToNearestGrid(float position)
    {
        float xDiff = position % gridSize;
        position -= xDiff;
        if (xDiff > (gridSize / 2))
        {
            position += gridSize;
        }

        return position;
    }

    public bool IsPointerOverUI()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }
        else
        {
            PointerEventData pe = new PointerEventData(EventSystem.current);
            pe.position = Input.mousePosition;
            List<RaycastResult> hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pe, hits);
            return hits.Count > 0;
        }
    }
}