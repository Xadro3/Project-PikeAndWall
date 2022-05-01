using UnityEngine;

public class Destructible : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject fracturedWall;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {


        if (collision.gameObject.tag == "SiegeProjectile")
        {
            Instantiate(fracturedWall, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public void Die()
    {
        Instantiate(fracturedWall, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
