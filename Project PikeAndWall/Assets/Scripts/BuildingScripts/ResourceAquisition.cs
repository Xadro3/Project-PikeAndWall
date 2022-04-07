using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceAquisition : MonoBehaviour
{

    private int aktuelleMenge = 0;

    public Text aktuelleMengeText;

    [SerializeField]bool destroyMode;

    //[SerializeField] private int startTime;
    [SerializeField] private int produktionsMenge;
    [SerializeField] private int timer;

    private void Start()
    {
        StartCoroutine(WaitBeforeProduction());        
        aktuelleMengeText = GameObject.Find("HolzMengeInteger").GetComponent<Text>();

    }

    private void Update()
    {
        aktuelleMengeText.text = aktuelleMenge.ToString();

    }

    private void Production()
    {

          aktuelleMenge = produktionsMenge + aktuelleMenge;
        
    }

    private IEnumerator WaitBeforeProduction()
    {
        yield return new WaitForSeconds(timer);
        Production();
        StartCoroutine(WaitBeforeProduction());
    }


}
