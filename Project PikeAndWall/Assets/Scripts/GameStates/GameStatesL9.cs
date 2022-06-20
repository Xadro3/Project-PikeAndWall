using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatesL9 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject eventSystem;

    // Update is called once per frame
    void Update()
    {
        if (GameEnviroment.Singleton.Units.Count == 0)
        {
            eventSystem.GetComponent<GameStates>().Lose();
        }
    }

    private void OnDestroy()
    {
        eventSystem.GetComponent<GameStates>().Win();
    }
}
