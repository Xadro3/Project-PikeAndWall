using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject lostScreen;
    GameObject winScreen;
    void Start()
    {
        lostScreen = GameObject.Find("LostScreen");
        winScreen = GameObject.Find("WinScreen");
    }

    // Update is called once per frame
    void Update()
    {
        if(GameEnviroment.Singleton.Units.Count == 0)
        {
           // lostScreen.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Lost");
        }
        if (GameEnviroment.Singleton.Enemies.Count == 0)
        {
            //winScreen.SetActive(true);
            Time.timeScale = 0;
            Debug.Log("Won");
        }
    }
}
