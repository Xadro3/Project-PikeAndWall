using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    // Start is called before the first frame update
    bool isInterrupted = false;
    public GameObject pauseMenu;
    void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!isInterrupted)
        {
            //Debug.Log("Escape!");
            pauseMenu.SetActive(true);
            InterruptGame();
            
        }
        if(Input.GetKeyDown(KeyCode.Escape) && isInterrupted)
        {
            pauseMenu.SetActive(false);
            ResumeGame();
        }
    }

    // Update is called once per frame
    public void InterruptGame()
    {
        isInterrupted = true;
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        isInterrupted = false;
        Time.timeScale = 1;
    }
}
