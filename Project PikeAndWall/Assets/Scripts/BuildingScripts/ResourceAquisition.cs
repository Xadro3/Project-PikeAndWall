using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceAquisition : MonoBehaviour
{

    private int aktuelleMenge = 0;

    public Text aktuelleMengeText;
    
    [SerializeField] private int startTime;
    [SerializeField] private int produktionsMenge;
    [SerializeField] private int timer;

    private void Start()
    {
         aktuelleMengeText = GameObject.Find("HolzMengeInteger").GetComponent<Text>();
    }

    void Update()
    {

        timer -= 1;
        if (timer == 0)
        {
            

            aktuelleMenge = produktionsMenge + aktuelleMenge;
            
            timer = startTime;
            
            aktuelleMengeText.text = aktuelleMenge.ToString();
            
        }

    }

}
