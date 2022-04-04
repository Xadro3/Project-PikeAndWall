using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedUnitsDictionary : MonoBehaviour
{

    public Dictionary<int, GameObject> selectedUnits = new Dictionary<int, GameObject>();

    public void AddSelectedUnits (GameObject selectedUnit)
    {
        int unitID = selectedUnit.GetInstanceID();

        if (!selectedUnits.ContainsKey(unitID))
        {
            selectedUnits.Add(unitID, selectedUnit);
            selectedUnit.AddComponent<UnitHighlighter>();
        }
    }

    public void RemoveUnitFromSelection (int unitID)
    {
        Destroy(selectedUnits[unitID].GetComponent<UnitHighlighter>());
        selectedUnits.Remove(unitID);
    }
    public void RemoveAllUnitsFromSelection()
    {
        foreach(KeyValuePair<int,GameObject> pair in selectedUnits)
        {
            if (pair.Value != null)
            {
                Destroy(selectedUnits[pair.Key].GetComponent<UnitHighlighter>());
            }
        }
        selectedUnits.Clear();
    }


}
