using System;
using System.Collections.Generic;
using UnityEngine;

public class FormationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject eventSystem;
    Dictionary<int, GameObject> selectedUnits;
    Dictionary<Vector3, GameObject> unitPlaces;
    GameObject[] unitObjects;


    public int selectedUnitNumber;

    public double ratioColumns;
    public double ratioRows;
    public double ratioQ;
    public double columns;
    public double rows;
    public GameObject lastInstance;

    Vector3 lastPosition;
    RaycastHit raycast;
    RaycastHit[] raycastHits;
    public GameObject formationPlacement;





    void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
        unitObjects = Array.Empty<GameObject>();
        unitPlaces = new Dictionary<Vector3, GameObject>();
        ratioQ = ratioColumns / ratioRows;



    }

    // Update is called once per frame
    void Update()
    {

        selectedUnitNumber = eventSystem.GetComponent<SelectedUnitsDictionary>().ReportNumberOfSelectedUnits();
        selectedUnits = eventSystem.GetComponent<SelectedUnitsDictionary>().selectedUnits;

        if (selectedUnitNumber != 0)
        {
            if (unitObjects.Length != 0)
            {
                Array.Clear(unitObjects, 0, unitObjects.Length);
            }
            unitObjects = new GameObject[selectedUnits.Count];
            selectedUnits.Values.CopyTo(unitObjects, 0);

        }

        columns = Math.Ceiling(Math.Sqrt(selectedUnitNumber * ratioQ));
        rows = Math.Ceiling(Math.Sqrt(selectedUnitNumber / ratioQ));

        if (Input.GetMouseButtonUp(1))
        {


            Ray destinationRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            raycastHits = Physics.RaycastAll(destinationRay, 50000f);

            foreach (RaycastHit raycastHit in raycastHits)
            {
                if (raycastHit.collider.gameObject.layer == 3)
                {
                    lastPosition = raycastHit.point;
                }
            }


            PlaceUnits();


        }



    }

    void PlaceUnits()
    {

        int index = 1;

        for (int i = 0; i < columns; i++)
        {

            for (int j = 0; j < rows; j++)
            {

                lastInstance = Instantiate(formationPlacement, new Vector3(lastPosition.x + (i * 2), lastPosition.y, lastPosition.z + (j * 2)), Quaternion.identity);

                Debug.Log("unitObjects is: " + unitObjects.Length + " unitPLaces is: " + unitPlaces.Count);

                if (selectedUnits.Count >= index)
                {
                    Debug.Log("i made it here");
                    unitPlaces.Add(lastInstance.transform.position, unitObjects[index - 1]);
                    index++;
                }

            }
        }

        Debug.Log("UnitPLaces is: " + unitPlaces.Count);

        foreach (KeyValuePair<Vector3, GameObject> entry in unitPlaces)
        {
            Debug.Log("Placed unit at: " + entry.Key);
            entry.Value.GetComponent<MovementCommandHandler>().MoveToDestinationMultiple(entry.Key);
        }
        unitPlaces.Clear();

    }


}
