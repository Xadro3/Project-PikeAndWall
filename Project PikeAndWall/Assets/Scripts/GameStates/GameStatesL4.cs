using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class GameStatesL4 : MonoBehaviour
{
    bool executed14 = true;
    public Flowchart flowchart;
    public GameObject eventSystem;

    // Update is called once per frame
    void Update()
    {

        if (GameObject.Find("LumberjackBuild(Clone)") != null && executed14)
        {
            flowchart.ExecuteBlock("Hütte gebaut");
            executed14 = false;
        }
        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Unit")
        {
            flowchart.ExecuteBlock("Armee in Ruine");
        }
    }
}
