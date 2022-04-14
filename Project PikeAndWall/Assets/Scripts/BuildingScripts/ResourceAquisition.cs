using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceAquisition : MonoBehaviour
{

    private int aktuelleMenge = 0;

    public Text aktuelleMengeText;

    [SerializeField] private int produktionsMenge;
    [SerializeField] private int timer;

    [SerializeField] bool wood;
    [SerializeField] bool stone;

    public List<ResourceValue> buildCost;
    public List<ResourceValue> producedResources;

    private void Start()
    {
        if (wood == true)
        {
            aktuelleMengeText = GameObject.Find("HolzMengeInteger").GetComponent<Text>();
        }

        if (stone == true)
        {
            aktuelleMengeText = GameObject.Find("StoneMengeInteger").GetComponent<Text>();
        }

        StartCoroutine(WaitBeforeProduction());        

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