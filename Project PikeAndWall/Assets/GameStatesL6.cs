using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class GameStatesL6 : MonoBehaviour
{
    // Start is called before the first frame update
    bool executed14 = true;
    bool executed = true;
    public Flowchart flowchart;
    public GameObject eventSystem;

    // Update is called once per frame
    private void Start()
    {
        InvokeRepeating("CheckBuildings", 0, 1.0f);
    }
    void Update()
    {

       
        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }

        foreach(KeyValuePair<int,GameObject> pair in eventSystem.GetComponent<SelectedUnitsDictionary>().selectedUnits)
        {
            if (pair.Value.name.Contains("Heavy")&&executed&&!executed14)
            {
                flowchart.ExecuteBlock("New Block1");
                executed = false;
            }
        }
    }
    void CheckBuildings()
    {
        if (GameObject.Find("MineIronPlaced(Clone)") != null && executed14 && GameObject.Find("MinePowderPlaced(Clone)") != null)
        {
            flowchart.ExecuteBlock("New Block");
            executed14 = false;
        }
    }
}
