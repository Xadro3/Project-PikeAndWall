using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    ResourceManagerUI resourceUI;

    Dictionary<ResourceType, int> resourceDictionary
        = new Dictionary<ResourceType, int>();

    public List<ResourceValue> initialResources = new List<ResourceValue>();

    private void Start()
    {
        resourceUI = FindObjectOfType<ResourceManagerUI>();
        PrepareResourceDictionary();
        SetInitialResourceValue();
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach(ResourceType resourceType in resourceDictionary.Keys)
        {
            UpdateUI(resourceType);
        }
    }

    private void UpdateUI(ResourceType resourceType)
    {
        resourceUI.SetResource(resourceType, resourceDictionary[resourceType]);
    }

    private void SetInitialResourceValue()
    {
        foreach (ResourceValue initialResourceValue in initialResources)
        {
            if (initialResourceValue.resourceType == ResourceType.None)
            {
                throw new ArgumentException("Resource cant be none");
            }
            resourceDictionary[initialResourceValue.resourceType] = initialResourceValue.resourceAmount;
        }
    }

    public void AddResource(List<ResourceValue> producedResources)
    {
        foreach (ResourceValue resourceVal in producedResources)
        {
            AddResource(resourceVal.resourceType, resourceVal.resourceAmount);
        }
    }

    public void AddResource(ResourceType resourceType, int resourceAmount)
    {
        resourceDictionary[resourceType] += resourceAmount;
        VerifyResourceAmount(resourceType);
        UpdateUI(resourceType);
    }

    private void VerifyResourceAmount(ResourceType resourceType)
    {
        if(resourceDictionary[resourceType] < 0)
        {
            throw new InvalidOperationException("Cant have resource less than 0" + resourceType);
        }
        
        //if(resourceDictionary[resourceType] < )
        //{

        //}
    }

    private void PrepareResourceDictionary()
    {
        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            if(resourceType == ResourceType.None)
                continue;
            resourceDictionary[resourceType] = 0;
        }
    }

    public bool CheckResourceAvailability(ResourceValue requiredResource)
    {
        return resourceDictionary[requiredResource.resourceType] >= requiredResource.resourceAmount;
    }

    public void SpendResource(List<ResourceValue> buildCost)
    {
        foreach (ResourceValue resourceValue in buildCost)
        {
            SpendResource(resourceValue.resourceType, resourceValue.resourceAmount);
        }
    }

    private void SpendResource(ResourceType resourceType, int resourceAmount)
    {
        resourceDictionary[resourceType] -= resourceAmount;
        VerifyResourceAmount(resourceType);
        UpdateUI(resourceType);
    }

    internal bool CheckResourceAvailability(ResourceValue resourceValue, object buildCost)
    {
        throw new NotImplementedException();
    }
}

[Serializable] public struct ResourceValue
{
    public ResourceType resourceType;
    [Min(0)]
    public int resourceAmount;
}
