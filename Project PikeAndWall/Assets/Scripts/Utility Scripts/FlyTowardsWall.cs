using UnityEngine;

public class FlyTowardsWall : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody thisBody;
    public float force;
    void Start()
    {
        thisBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("I am here");

            thisBody.AddForce(transform.position.x * force, transform.position.y * (force / 2), 0, ForceMode.Impulse);

        }
    }
}
