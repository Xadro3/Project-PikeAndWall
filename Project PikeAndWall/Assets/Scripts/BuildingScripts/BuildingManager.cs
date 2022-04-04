using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private Material[] materials;

    public int raycastLength;

    public GameObject[] blueprint;
    public GameObject pendingBuilding;

    private Vector3 position;

    private RaycastHit hit;

    [SerializeField] private LayerMask layerMask;

    public float rotateAmount;

    public float gridSize;
    bool gridOn;

    public bool canPlace = true;

    [SerializeField] private Toggle gridToggle;

   
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

            
            if(Input.GetMouseButtonDown(0) && canPlace)
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
     //   pendingBuilding.GetComponent<MeshRenderer>().material = materials[2];
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
        if (pendingBuilding == null)
        {
            pendingBuilding = Instantiate(blueprint[index], position, transform.rotation);
        }

        //if (pendingBuilding != null)
        //{
        //    Destroy(pendingBuilding);
        //}
        //pendingBuilding = Instantiate(blueprint[index], position, transform.rotation);

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

}
