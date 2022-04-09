using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FormationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject eventSystem;
    Dictionary<int, GameObject> selectedUnits;
    Dictionary<Vector3, GameObject> unitPlaces;

    public int selectedUnitNumber;

    public double ratioColumns;
    public double ratioRows;
    public double ratioQ;
    public double columns;
    public double rows;

    int lastRow;
    int lastColumn;
    int unitsToPlace;
    Vector3 lastPosition;
    RaycastHit raycast;
    public GameObject formationPlacement;





    void Start()
    {
        eventSystem = GameObject.Find("EventSystem");


        ratioQ = ratioColumns / ratioRows;



    }

    // Update is called once per frame
    void Update()
    {

        selectedUnitNumber = eventSystem.GetComponent<SelectedUnitsDictionary>().ReportNumberOfSelectedUnits();
        selectedUnits = eventSystem.GetComponent<SelectedUnitsDictionary>().selectedUnits;





        if (selectedUnitNumber > 1)
        {

            columns = Math.Ceiling(Math.Sqrt(selectedUnitNumber * ratioQ));
            rows = Math.Ceiling(Math.Sqrt(selectedUnitNumber / ratioQ));

            if (Input.GetMouseButtonUp(1))
            {
                Debug.Log("I am here");

                Ray destinationRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(destinationRay, out raycast, 50000f))
                {
                    lastPosition = raycast.point;
                    PlaceUnits();
                }

            }

        }

    }

    void PlaceUnits()
    {

        

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                Debug.Log("PlaceUnits at: " + j);
                Instantiate(formationPlacement, new Vector3(lastPosition.x + i+5, lastPosition.y, lastPosition.z + j+5),Quaternion.identity);
            }
        }
    }
    
}
