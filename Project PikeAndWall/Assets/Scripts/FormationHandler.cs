using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationHandler : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject eventSystem;

    int selectedUnitNumber;
    int placedUnits;

    Vector3 transformPosition;
    Vector3 mousePosition;

    void Start()
    {
        eventSystem = GameObject.Find("EventSystem");
        selectedUnitNumber = eventSystem.GetComponent<SelectedUnitsDictionary>().ReportNumberOfSelectedUnits();
        transformPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;

        if (selectedUnitNumber > 1)
        {

        }
    }
}
