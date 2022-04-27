using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAquisition : MonoBehaviour
{

    [SerializeField] private int timer;


    ResourceManager resourceManager;
    public List<ResourceValue> buildCost;
    public List<ResourceValue> producedResources;

    private void Start()
    {
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();

        StartCoroutine(WaitBeforeProduction());

        resourceManager.SpendResource(buildCost);
    }

    private IEnumerator WaitBeforeProduction()
    {
        yield return new WaitForSeconds(timer);
        resourceManager.AddResource(producedResources);
        StartCoroutine(WaitBeforeProduction());
    }
}