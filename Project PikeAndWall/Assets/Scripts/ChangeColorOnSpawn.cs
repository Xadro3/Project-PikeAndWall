using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOnSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer renderer;
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        renderer.material.SetColor("_Color", Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
