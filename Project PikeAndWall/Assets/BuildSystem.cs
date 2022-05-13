using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    // Start is called before the first frame update
    RaycastHit[] raycastHits;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            raycastHits = Physics.RaycastAll(ray, 5000f);
            foreach(RaycastHit hit in raycastHits)
            {
                if (hit.collider.gameObject.tag == "UIQuad")
                {
                    hit.collider.gameObject.GetComponentInParent<BuildUnits>().QueueUnit();
                    break;
                }
            }
        }
        
    }
}
