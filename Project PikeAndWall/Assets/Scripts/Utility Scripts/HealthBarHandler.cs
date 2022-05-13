using UnityEngine;

public class HealthBarHandler : MonoBehaviour
{
    // Start is called before the first frame update
    Renderer renderer;

    void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        HideHealthBar();

    }

    private void Update()
    {
        
    }
    // Update is called once per frame
    public void ShowHealthBar()
    {
        renderer.enabled = true;
    }
    public void HideHealthBar()
    {
        renderer.enabled = false;
    }
    public void SetHealth(float health)
    {



        if (renderer != null)
        {
            renderer.material.SetFloat(("_Health"), health);
        }
    }
}
