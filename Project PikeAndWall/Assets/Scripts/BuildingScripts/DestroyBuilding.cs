using System.Collections.Generic;
using UnityEngine;

public class DestroyBuilding : MonoBehaviour
{

    [SerializeField] GameObject destroyEffekt;
    [SerializeField] float destroyEffektTime;
    BuildingManager buildingManager;
    ResourceManager resourceManager;

    public List<ResourceValue> getResouceBackInt;


    AudioSource audioSource;
    [SerializeField] AudioClip destroyClip;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        resourceManager = GameObject.Find("ResourceManager").GetComponent<ResourceManager>();
    }

    private void OnMouseDown()
    {
        if (buildingManager.destroyMode == true && !buildingManager.IsPointerOverUI())
        {
            DestroyBuildingEffekt();
            GetResourcesBack();
        }
    }

    private void DestroyBuildingEffekt()
    {

        if (destroyEffekt != null)
        {
            Destroy(Instantiate(destroyEffekt, transform.position, transform.rotation), destroyEffektTime);
            audioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
            audioSource.PlayOneShot(destroyClip);
        }
        Destroy(gameObject);
    }

    public void GetResourcesBack()
    {
        resourceManager.AddResource(getResouceBackInt);
    }

}
