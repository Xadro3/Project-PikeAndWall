using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    public List<Transform> playerUnits;
    public bool charge;
    public Transform objective;
    public bool isGuard;
    States currentState;
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        animator = this.GetComponent<Animator>();
        currentState = new Idle(this.gameObject, agent, animator, GameEnviroment.Singleton.Units, GetComponent<UnitClass>().range, isGuard, charge, objective);
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();
    }
}
