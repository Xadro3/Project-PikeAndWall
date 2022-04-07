using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


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

    [SerializeField] private Material[] materials;   
    [SerializeField] private Toggle gridToggle;
    [SerializeField] private LayerMask layerMask;
    
    //[SerializeField] private Toggle destroyToggle;

    public bool destroyMode;

    void Update()
    {
        if(pendingBuilding != null)
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

            //if (!IsPointerOverUI())
            //{
            //    canPlace = false;
            //}

            if(Input.GetMouseButtonDown(0) && canPlace &&! IsPointerOverUI())
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
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else
        {
            gridOn = false;
        }
    }

    public void ToggleDestoryBuild(bool destroyToggle)
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
        if(xDiff > (gridSize / 2))
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
