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
    private bool toBattle;
    
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
                if (raycastHit.transform.CompareTag("Enemy"))
                {
                    Hitbox targetHitbox = raycastHit.transform.GetComponentInChildren<Hitbox>();

                    if (targetsInRange.Contains(targetHitbox))
                    {
                        unit.enemyInRange = true;
                        unit.SetTarget(targetHitbox);
                        agent.ResetPath();
                        //agent.isStopped = true;
                        
                    }
                    CheckTargetHelper(targetHitbox, raycastHit);
                    agent.isStopped = false;
                    toBattle = true;
                }
                else
                {
                    toBattle = false;
                }
            }
            unit.SetTarget(null);
            agent.isStopped = false;
            
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
            if (unit.CompareTag("Unit"))
            {
                if (collision.CompareTag("Enemy") && toBattle)
                {
                    targetsInRange.Add(hitbox);
                    unit.enemyInRange = true;
                    agent.ResetPath();
                    //agent.isStopped = true;
                    unit.SetTarget(hitbox);
                }
            }
            if (unit.CompareTag("Enemy"))
            {
                if (collision.CompareTag("Player") && toBattle)
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
            if (unit.CompareTag("Unit"))
            {
                if (collision.CompareTag("Enemy"))
                {
                    targetsInRange.Remove(hitbox);
                    unit.SetTarget(null);
                    unit.enemyInRange = false;
                    agent.isStopped = false;
                }
            }
            if (unit.CompareTag("Enemy"))
            {
                if (collision.CompareTag("Player"))
                {
                    targetsInRange.Remove(hitbox);
                    unit.SetTarget(null);
                    unit.enemyInRange = false;
                }
            }
        }
    }


}
