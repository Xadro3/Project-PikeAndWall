using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private TMP_Text resourceAmount;
    [SerializeField] private ResourceType resourceType;

    public ResourceType ResourceType { get => resourceType; }

    private void Start()
    {
        if (resourceType == ResourceType.None)
        {
            throw new System.ArgumentException("Resource type cannt be None in "+ gameObject.name);
        }

    }

    public void SetValue(int val)
    {
        resourceAmount.text = val.ToString();
    }
}

public enum ResourceType
{
    None,
    Wood,
    Stone,
    BuildingSlots
}
