using System;
using System.Collections.Generic;
using System.Collections;
using Fungus;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class TargetHandler : MonoBehaviour
{

    private UnitClass unit;
    public List<Hitbox> targetsInRange;
    RaycastHit[] raycastHits;

    private Hitbox targetHitbox;
    private RaycastHit raycastHit;
    private NavMeshAgent agent;
    private bool toBattle;
    private int layerMask = 1 << 7;

    //List <GameObject> currentCollisions = new List <GameObject> ();

    void Awake()
    {
        unit = GetComponent<UnitClass>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        StartCoroutine(FindNewTarget());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && transform.TryGetComponent(out UnitHighlighter amISelected))
        {
            unit.enemyInRange = false;
            Ray destinationRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            raycastHits = Physics.RaycastAll(destinationRay, 50000f);
            
            foreach (RaycastHit raycastHit in raycastHits)
            {
                if (raycastHit.transform.CompareTag("Enemy"))
                {
                    Hitbox targetHitbox = raycastHit.transform.GetComponentInChildren<Hitbox>();
                    unit.SetTarget(targetHitbox);
                    agent.isStopped = false;
                    SetTargetValue(targetHitbox, raycastHit);
                    if (IsInRange(targetHitbox))
                    {
                        unit.enemyInRange = true;
                        agent.ResetPath();
                        unit.unitAttack.StartAttack();
                        //agent.isStopped = true;
                    }
                    InvokeRepeating("MoveToAttack", 0, 0.1f);
                    StartCoroutine(WaitingToAttack(targetHitbox));
                    break;
                }
            }
            agent.isStopped = false;
        }
        
    }

    public void MoveToAttack()
    {
        if ((Vector3.Distance(transform.position, targetHitbox.transform.position)) > unit.range / 2)
        {
            agent.SetDestination(targetHitbox.transform.position);
        }
        else
        {
            CancelInvoke("MoveToAttack");
            agent.ResetPath();
        }

        //unit.unitAttack.StartAttack();
    }
    
    private IEnumerator FindNewTarget()
    {
        while (ClearDestroyedTargetsInRange(targetHitbox) <= 0)
        {
            yield return new WaitForSeconds(1.0f);
        }
        AttackNewTarget();

    }
    
    private IEnumerator WaitingToAttack(Hitbox targetHitbox)
    {
        while (!IsInRange(targetHitbox))
        {
            yield return new WaitForSeconds(0.1f);
        }
        unit.enemyInRange = true;
        agent.ResetPath();
        unit.unitAttack.StartAttack();
        yield break;
    }

    public int ClearDestroyedTargetsInRange(Hitbox targetHitbox)
    {
        int i = targetsInRange.RemoveAll(targetHitbox => targetHitbox == null);
        return i;
    }

    public void AttackNewTarget()
    {
        Hitbox newTarget = targetsInRange[Random.Range(0, targetsInRange.Count)];
        unit.SetTarget(newTarget);
        StartCoroutine(WaitingToAttack(newTarget));
        StartCoroutine(FindNewTarget());
        //unit.enemyInRange = true;
        //agent.ResetPath();
        //unit.unitAttack.StartAttack();

    }
    
    public void SetTargetValue(Hitbox targetHitbox, RaycastHit raycastHit)
    {
        this.targetHitbox = targetHitbox;
        this.raycastHit = raycastHit;
    }

    private bool IsInRange(Hitbox targetHitbox)
    {
        if (targetsInRange.Contains(targetHitbox))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox))
        {
            if (unit.CompareTag("Unit"))
            {
                if (collision.CompareTag("Enemy"))
                {
                    targetsInRange.Add(hitbox);
                    //unit.enemyInRange = true;
                    //agent.ResetPath();
                    //agent.isStopped = true;
                    //unit.SetTarget(hitbox);
                }
            }

            if (hitbox == targetHitbox)
            {
                unit.enemyInRange = true;
            }
            if (unit.CompareTag("Enemy"))
            {
                if (collision.CompareTag("Player"))
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
                    //unit.SetTarget(null);
                    //unit.enemyInRange = false;
                    agent.isStopped = false;
                }
            }
            //if (hitbox == targetHitbox)
            //{
            //    unit.enemyInRange = false;
            //}
            if (unit.CompareTag("Enemy") && hitbox == targetHitbox)
            {
                if (collision.CompareTag("Player"))
                {
                    targetsInRange.Remove(hitbox);
                    //unit.SetTarget(null);
                    unit.enemyInRange = false;
                }
            }
        }
    }


}
