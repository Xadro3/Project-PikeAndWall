using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject lostScreen;
    public GameObject winScreen;
    [SerializeField] int timeScale = 0;
    void Start()
    {
        
        //Time.timeScale = timeScale;
        //lostScreen = GameObject.Find("LostScreen");
        //winScreen = GameObject.Find("WinScreen");
    }

    // Update is called once per frame
    void Update()
    {
        //if(GameEnviroment.Singleton.Units.Count == 0)
        //{
        //    lostScreen.SetActive(true);
        //    Time.timeScale = timeScale;
        //    Debug.Log("Lost");
        //}
        //if (GameEnviroment.Singleton.Enemies.Count == 0)
        //{
        //    winScreen.SetActive(true);
        //    Time.timeScale = timeScale;
        //    Debug.Log("Won");
        //}
    }

    public void Win()
    {
        winScreen.SetActive(true);
           Time.timeScale = timeScale;
           Debug.Log("Won");
    }

    public void Lose()
    {
        lostScreen.SetActive(true);
           Time.timeScale = timeScale;
           Debug.Log("Lost");
    }
}
