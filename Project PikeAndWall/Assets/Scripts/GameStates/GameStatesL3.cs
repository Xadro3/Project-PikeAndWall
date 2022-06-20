using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
public class GameStatesL3 : MonoBehaviour
{
    bool executed14 = true;
    public Flowchart flowchart;
    public GameObject eventSystem;

    // Update is called once per frame
    void Update()
    {
        if (GameEnviroment.Singleton.Enemies.Count == 0 && executed14)
        {
            flowchart.ExecuteBlock("Nach Schlacht (Copy)");
            executed14 = false;
        }
        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }
}
