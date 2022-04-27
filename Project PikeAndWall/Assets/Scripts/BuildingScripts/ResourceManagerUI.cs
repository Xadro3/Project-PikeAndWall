using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManagerUI : MonoBehaviour
{

    Dictionary<ResourceType, ResourceUI> resourceUiDictionary
        = new Dictionary<ResourceType, ResourceUI>();

    private void Awake()
    {
        PrepareResourceDictonary();
    }

    private void PrepareResourceDictonary()
    {
        foreach (ResourceUI resourceUIReference in GetComponentsInChildren<ResourceUI>())
        {
            if (resourceUiDictionary.ContainsKey(resourceUIReference.ResourceType))
            {
                throw new ArgumentException("Dictionary already contains a" + resourceUIReference.ResourceType.ToString());
            }
            resourceUiDictionary[resourceUIReference.ResourceType] = resourceUIReference;
            SetResource(resourceUIReference.ResourceType, 0);
        }
    }

    public void SetResource(ResourceType resourceType, int val)
    {
        try
        {
            resourceUiDictionary[resourceType].SetValue(val);
        }
        catch (Exception)
        {
            throw new Exception("Dictionary doesnt contain a ReferenceUI for " + resourceType);
        }
    }

}