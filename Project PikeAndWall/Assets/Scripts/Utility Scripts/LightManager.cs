using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class LightManager : MonoBehaviour
{

    [SerializeField] private Light directionalLight;
    [SerializeField] private LightingPreset preset;
    [SerializeField, Range(0, 24)] private float timeOfDay; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (preset == null)
        {
            return;
        }
        if (Application.isPlaying)
        {
            //timeOfDay += Time.deltaTime;
            //timeOfDay %= 24;
            
        }
        else
        {
            UpdateLight(timeOfDay / 24f);
        }
    }

    private void UpdateLight(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.FogColor.Evaluate(timePercent);
        
        if(directionalLight != null)
        {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);
            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }
    }
}
