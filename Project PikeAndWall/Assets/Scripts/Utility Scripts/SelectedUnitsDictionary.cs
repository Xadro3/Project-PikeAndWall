using System.Collections.Generic;
using UnityEngine;

public class SelectedUnitsDictionary : MonoBehaviour
{

    public Dictionary<int, GameObject> selectedUnits = new Dictionary<int, GameObject>();
    public ResourceManager resourceManager;
    public GameObject upgradedBowman;
    public GameObject upgradedPikeman;
    public GameObject upgradedCavalry;
    public Dictionary<ResourceType, int> upgradeCosts
        = new Dictionary<ResourceType, int>();
    public int[] returnAR = new int[3];

    private void Start()
    {
        upgradeCosts[ResourceType.Food] = 0;
        upgradeCosts[ResourceType.Iron] = 0;
        upgradeCosts[ResourceType.BlackPowder] = 0;

        InvokeRepeating("ReportUnitTypesSelected", 0, 1.0f);
        InvokeRepeating("RemoveDead", 0, 0.1f);
    }
    void RemoveDead()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedUnits)
        {
            Debug.Log(pair.Value);

            if (pair.Value==null)
            {
                RemoveUnitFromSelection(pair.Key);
            }
        }
    }
    public void AddSelectedUnits(GameObject selectedUnit)
    {


        int unitID = selectedUnit.GetInstanceID();

        if (!selectedUnits.ContainsKey(unitID))
        {
            selectedUnits.Add(unitID, selectedUnit);
            //Debug.Log(selectedUnits.Count);
            selectedUnit.AddComponent<UnitHighlighter>();
        }
    }

    public void RemoveUnitFromSelection(int unitID)
    {
        if (selectedUnits[unitID]!=null && selectedUnits[unitID].GetComponent<UnitHighlighter>() == null)
        {
            Destroy(selectedUnits[unitID].GetComponent<UnitHighlighter>());
        }
        
        selectedUnits.Remove(unitID);
    }
    public void RemoveAllUnitsFromSelection()
    {
        foreach (KeyValuePair<int, GameObject> pair in selectedUnits)
        {
            if (pair.Value != null)
            {
                Destroy(selectedUnits[pair.Key].GetComponent<UnitHighlighter>());
            }
        }
        selectedUnits.Clear();
    }
    public int ReportNumberOfSelectedUnits()
    {
        return selectedUnits.Count;
    }

    public void UpgradeSelectedUnits()
    {

        foreach (KeyValuePair<int, GameObject> pair in selectedUnits)
        {
            bool canafford = false;
            if (pair.Value.tag == "Unit")
            {
                if (pair.Value.GetComponent<UnitClass>().className == "Pikeman")
                {
                    foreach (ResourceValue value in pair.Value.GetComponent<UnitClass>().upgradeCost)
                    {
                        if (!(canafford = resourceManager.CheckResourceAvailability(value)))
                        {
                            canafford = false;
                            break;
                        }
                        else
                        {
                            canafford = true;
                        }
                    }
                    if (canafford) {
                        resourceManager.SpendResource(pair.Value.GetComponent<UnitClass>().upgradeCost);
                        Instantiate(upgradedPikeman, pair.Value.transform.position, pair.Value.transform.rotation);
                        GameEnviroment.Singleton.Units.Remove(pair.Value);
                        Destroy(pair.Value);
                        continue;
                    }
                    
                }
                if (pair.Value.GetComponent<UnitClass>().className == "Archer")
                {
                    foreach (ResourceValue value in pair.Value.GetComponent<UnitClass>().upgradeCost)
                    {
                        if (!(canafford = resourceManager.CheckResourceAvailability(value)))
                        {
                            canafford = false;
                            break;
                        }
                        else
                        {
                            canafford = true;
                        }
                    }
                    if (canafford)
                    {
                        resourceManager.SpendResource(pair.Value.GetComponent<UnitClass>().upgradeCost);
                        Instantiate(upgradedBowman, pair.Value.transform.position, pair.Value.transform.rotation);
                        GameEnviroment.Singleton.Units.Remove(pair.Value);
                        Destroy(pair.Value);
                        continue;
                    }
                }
                if (pair.Value.GetComponent<UnitClass>().className == "Cavalry")
                {
                    foreach (ResourceValue value in pair.Value.GetComponent<UnitClass>().upgradeCost)
                    {
                        if (!(canafford = resourceManager.CheckResourceAvailability(value)))
                        {
                            canafford = false;
                            break;
                        }
                        else
                        {
                            canafford = true;
                        }
                    }
                    if (canafford)
                    {
                        resourceManager.SpendResource(pair.Value.GetComponent<UnitClass>().upgradeCost);
                        Instantiate(upgradedCavalry, pair.Value.transform.position, pair.Value.transform.rotation);
                        GameEnviroment.Singleton.Units.Remove(pair.Value);
                        Destroy(pair.Value);
                        continue;
                    }
                }
            }


        }
        RemoveAllUnitsFromSelection();
    }
    public void ReportUnitTypesSelected()
    {
        
        
        foreach (KeyValuePair<int, GameObject> pair in selectedUnits)
        {
            if (pair.Value.GetComponent<UnitClass>() != null)
            {
                if (pair.Value.GetComponent<UnitClass>().className == "Pikeman")
                {
                    foreach (ResourceValue resourceValue in pair.Value.GetComponent<UnitClass>().upgradeCost)
                    {
                        upgradeCosts[resourceValue.resourceType] += resourceValue.resourceAmount;
                    }
                    continue;
                }
                if (pair.Value.GetComponent<UnitClass>().className == "Archer")
                {
                    foreach (ResourceValue resourceValue in pair.Value.GetComponent<UnitClass>().upgradeCost)
                    {
                        upgradeCosts[resourceValue.resourceType] += resourceValue.resourceAmount;
                    }

                    continue;
                }
                if (pair.Value.GetComponent<UnitClass>().className == "Cavalry")
                {
                    foreach (ResourceValue resourceValue in pair.Value.GetComponent<UnitClass>().upgradeCost)
                    {
                        upgradeCosts[resourceValue.resourceType] += resourceValue.resourceAmount;
                    }

                    continue;
                }
            }
            
        }

        
        upgradeCosts.TryGetValue(ResourceType.Food, out returnAR[0]);
        upgradeCosts.TryGetValue(ResourceType.Iron, out returnAR[1]);
        upgradeCosts.TryGetValue(ResourceType.BlackPowder, out returnAR[2]);
        upgradeCosts[ResourceType.Food] = 0;
        upgradeCosts[ResourceType.Iron] = 0;
        upgradeCosts[ResourceType.BlackPowder] = 0;
    
        

        
        
        
    }
}

            
