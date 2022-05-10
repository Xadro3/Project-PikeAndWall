using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetHandler : MonoBehaviour
{

    private UnitClass unit;
    public List<Hitbox> targetsInRange;
    RaycastHit[] raycastHits;

    private Hitbox targetHitbox;
    private RaycastHit raycastHit;
    private NavMeshAgent agent;
    
    //List <GameObject> currentCollisions = new List <GameObject> ();

    void Awake()
    {
        unit = GetComponent<UnitClass>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray destinationRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            raycastHits = Physics.RaycastAll(destinationRay, 50000f);
            
            foreach (RaycastHit raycastHit in raycastHits)
            {
                if (raycastHit.transform.tag == "Enemy")
                {
                    Hitbox targetHitbox = raycastHit.transform.GetComponentInChildren<Hitbox>();

                    if (targetsInRange.Contains(targetHitbox))
                    {
                        unit.enemyInRange = true;
                        unit.SetTarget(targetHitbox);
                        agent.isStopped = true;
                    }
                    else
                    {
                        //unit.enemyInRange = false;
                        //unit.SetTarget(null);
                        agent.isStopped = false;
                    }
                    CheckTargetHelper(targetHitbox, raycastHit);

                }
            }
        }
        CheckTarget(targetHitbox, raycastHit);
    }


    public void CheckTargetHelper(Hitbox targetHitbox, RaycastHit raycastHit)
    {
        this.targetHitbox = targetHitbox;
        this.raycastHit = raycastHit;
    }

    private void CheckTarget(Hitbox targetHitbox, RaycastHit raycastHit)
    {
        if (targetsInRange.Contains(targetHitbox))
        {
            unit.enemyInRange = true;
        }
        else
        {
            unit.enemyInRange = false;
            targetsInRange.Remove(targetHitbox);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox))
        {
            if (unit.tag == "Unit")
            {
                if (collision.tag == "Enemy")
                {
                    targetsInRange.Add(hitbox);
                    unit.enemyInRange = true;
                    agent.isStopped = true;
                    unit.SetTarget(hitbox);
                }
            }
            if (unit.tag == "Enemy")
            {
                if (collision.tag == "Player")
                {
                    targetsInRange.Add(hitbox);
                    unit.enemyInRange = true;
                    unit.SetTarget(hitbox);
                }
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox))
        {
            if (unit.tag == "Unit")
            {
                if (collision.tag == "Enemy")
                {
                    targetsInRange.Remove(hitbox);
                    unit.SetTarget(null);
                    unit.enemyInRange = false;
                }
            }
            if (unit.tag == "Enemy")
            {
                if (collision.tag == "Player")
                {
                    targetsInRange.Remove(hitbox);
                    unit.SetTarget(null);
                    unit.enemyInRange = false;
                }
            }
        }
    }


}
