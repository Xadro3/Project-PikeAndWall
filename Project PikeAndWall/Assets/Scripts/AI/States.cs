using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class States 
{
   
    public enum STATE
    {
        IDLE, ATTACK, PURSUE, RETREAT, PATROL, CHARGE
    };
    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    };

    public STATE name;
    protected EVENT stage; //References line 14 "Stages" e.g ENTER
    protected GameObject npc; 
    protected Animator animator;
    //protected Transform playerUnit; //closest unit from the player
    protected List<GameObject> playerUnits; // all Player Units
    protected States nextState;
    protected NavMeshAgent agent;
    protected Transform objective; //position to charge to
    protected float aggroRange = 50f;
    protected float attackRange;
    protected bool isGuard; //prevent pursuit
    protected bool charge; 
    protected bool isPatrol; //prevent all enemy units to patrol 
    protected bool gettingAttacked;
    public States(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAttacked)
    {
        npc = _npc;
        agent = _agent;
        animator = _anim;
        playerUnits = _playerUnits;
        attackRange = _attackRange;
        isGuard = _isGuard;
        charge = _charge;
        objective = _objective;
        stage = EVENT.ENTER;
        isPatrol = _isPatrol;
        gettingAttacked = _gettingAttacked;
    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }
    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

    public States Process()
    {
        if(stage == EVENT.ENTER)
        {
            Enter();
        }

        if(stage == EVENT.UPDATE)
        {
            Update();
        }
        if(stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }
        return this;
    }

}

public class Idle : States
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAtacked)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard,_charge, _objective, _isPatrol, _gettingAtacked)
    {
        name = STATE.IDLE;
    }
    public override void Enter()
    {
        animator.SetTrigger("isIdle");
        Debug.Log("AI is in Idle");
        base.Enter();
    }

    public override void Update()
    {

       

        
            Vector3 npcPos = npc.transform.position;
           
            foreach (GameObject unit in playerUnits)
            {
                if ((Vector3.Distance(unit.transform.position, npcPos)) < aggroRange && !isGuard)   //if any unit is within range, start pursuit
                {
                   nextState = new Pursue(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked ,unit);
                   Debug.Log("AI transitioning to Pursue");
                   stage = EVENT.EXIT;
                }
                if((Vector3.Distance(unit.transform.position, npcPos)) <= attackRange && isGuard)
                {
                   nextState = new Attack(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked, unit, npcPos); // transition to attacking
                stage = EVENT.EXIT;
            }
            }

            if (Random.Range(0, 1000) < 10 && isPatrol && !isGuard) //if this unit has patrol flag, random chance to start patroling
            {
                nextState = new Patrol(npc,agent,animator,playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
                Debug.Log("AI is transitioning to Patrol");
                stage = EVENT.EXIT;
            }
            if (npc.GetComponent<Ai>().charge) // if charge flag is set, charge
            {
                nextState = new Charge(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
                Debug.Log("Ai is Transitioning to Charge");
                    stage = EVENT.EXIT;
            }
    }
        
        
    

    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}

public class Pursue: States
{
    GameObject closestUnit;
    Vector3 origin;
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAtacked, GameObject _closestUnit)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard, _charge, _objective, _isPatrol, _gettingAtacked)
    {
        name = STATE.PURSUE;
        agent.speed = agent.speed +4.5f; ;
        agent.isStopped = false;
        closestUnit = _closestUnit;
        
    }

    public  override void Enter()
    {
        Debug.Log("AI is in Pursuit");
        animator.SetTrigger("isRunning");
        origin = npc.transform.position;
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(closestUnit.transform.position); //follow closest unit

        if (agent.hasPath)
        {
            foreach (GameObject unit in playerUnits)
            {
                if (Vector3.Distance(unit.transform.position, npc.transform.position) < Vector3.Distance(closestUnit.transform.position, npc.transform.position)) // find closest unit
                {
                    closestUnit = unit;
                }
            }
            if(npc.GetComponent<Ai>().charge && Vector3.Distance(origin, npc.transform.position) > 25)
            {
                nextState = new Charge(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
                stage = EVENT.EXIT;
            }

            if (Vector3.Distance(closestUnit.transform.position, npc.transform.position) <= attackRange)
            {
                nextState = new Attack(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked, closestUnit,origin); // transition to attacking
                stage = EVENT.EXIT;
            }

            if (Vector3.Distance(origin, npc.transform.position) > 50 && !gettingAttacked) // retreat if not in combat and too far from origin
            {
                nextState = new Retreat(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked ,origin, closestUnit);
                stage = EVENT.EXIT;
            }
            
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("isRunning");
        base.Exit();
    }


}

public class Patrol: States
{
    int currentIndex = -1;
    int patrolPasses = 0;
    Vector3 origin;
    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAtacked)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard,_charge, _objective, _isPatrol, _gettingAtacked)
    {
        name = STATE.PATROL;
        agent.speed = agent.speed-3.5f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        origin = npc.transform.position;
        Debug.Log("AI is in Patrol");
        currentIndex = 0;
        animator.SetTrigger("isWalking");
        base.Enter();
    }


    public override void Update()
    {
        if (npc.GetComponent<Ai>().charge)
        {
            nextState = new Charge(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
            stage = EVENT.EXIT;
        }

        foreach (GameObject unit in playerUnits)
        {
            if ((Vector3.Distance(unit.transform.position, npc.transform.position)) < aggroRange)   //if any unit is within range, start pursuit
            {
                nextState = new Pursue(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked ,unit);
                Debug.Log("AI transitioning to Pursue");
                stage = EVENT.EXIT;
            }
        }

        if (agent.remainingDistance < 1)
        {
            if(currentIndex >= GameEnviroment.Singleton.Perimeter.Count-1) //walk through waypoints and increase counter for break off
            {
                patrolPasses++;
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            agent.SetDestination(GameEnviroment.Singleton.Perimeter[currentIndex].transform.position);
        }

        if(patrolPasses >= 5) // break off Patrol after 5 passes
        {
            agent.SetDestination(origin);
            Debug.Log("AI is going back to Idle");
            nextState = new Idle(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
            stage = EVENT.EXIT;
        }
        
    }


    public override void Exit()
    {
        animator.ResetTrigger("isWalking");
        base.Exit();
    }

}
public class Charge : States
{
    public Charge(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAtacked)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard, _charge, _objective, _isPatrol, _gettingAtacked)
    {
        name = STATE.CHARGE;
        agent.speed = agent.speed + 4.5f; ;
        agent.isStopped = false;

    }

    public override void Enter()
    {
        Debug.Log("AI is Charging");
        animator.SetTrigger("isRunning");
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(objective.position);

        foreach (GameObject unit in playerUnits)
        {
            if ((Vector3.Distance(unit.transform.position, npc.transform.position)) < aggroRange / 2)   //if any unit is within range, start pursuit
            {
                nextState = new Pursue(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked, unit);
                Debug.Log("AI transitioning to Pursue");
                stage = EVENT.EXIT;
            }
        }
    }
}

public class Retreat : States
{
        Vector3 origin;
        GameObject closestUnit;
        public Retreat(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAtacked, Vector3 _origin, GameObject _closestUnit)
                    : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard, _charge, _objective, _isPatrol, _gettingAtacked)
        {
            name = STATE.RETREAT;
            agent.speed = agent.speed + 4.5f; ;
            agent.isStopped = false;
            origin = _origin;
            closestUnit = _closestUnit;
        }

        public override void Enter()
        {
            Debug.Log("Ai is Retreating");
            animator.SetTrigger("isRunning");
            base.Enter();
        }

        public override void Update()
        {
            if (!gettingAttacked) //if not in combat, return to idle 
            {
            if (agent.destination != origin)
            {
                Debug.Log("AI is returning to Origin");
                agent.SetDestination(origin);
            }
                if(origin == npc.transform.position)
            {
                nextState = new Idle(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
                stage = EVENT.EXIT;
            }
            }
            else
            {
                Debug.Log("AI  is continuing pursuit"); // ignore max distance from origin in favor of pursuit
                nextState = new Pursue(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked, closestUnit);
                stage = EVENT.EXIT;

            }

            if (npc.GetComponent<Ai>().charge) // if charge flag is set, charge
            {
            nextState = new Charge(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
            Debug.Log("Ai is Transitioning to Charge");
            stage = EVENT.EXIT;
            }

    }

        public override void Exit()
        {
            animator.ResetTrigger("isRunning");
            base.Exit();

        }

}
public class Attack : States
{
    GameObject closestUnit;
    Vector3 origin;
    Vector3 closestUnitPosition;
        public Attack(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective, bool _isPatrol, bool _gettingAtacked, GameObject _closestUnit, Vector3 _origin)
                    : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard, _charge, _objective, _isPatrol, _gettingAtacked)
        {

        closestUnit = _closestUnit;
        origin = _origin;
        name = STATE.ATTACK;

        }

    public override void Enter()
    {
        Debug.Log("AI is attacking");
        animator.SetTrigger("isAttacking");
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        
        if(closestUnit != null)
        {
            closestUnitPosition = closestUnit.transform.position;

            if (Vector3.Distance(closestUnit.transform.position, npc.transform.position) <= attackRange)
            {
                npc.GetComponent<UnitClass>().enemyInRange = true;
                npc.GetComponent<UnitClass>().SetTarget(closestUnit.GetComponentInChildren<Hitbox>());
                npc.GetComponent<UnitClass>().unitAttack.StartAttack();
            }
            else
            {
                npc.GetComponent<UnitClass>().enemyInRange = false;
                nextState = new Pursue(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked, closestUnit);
                stage = EVENT.EXIT;
            }
        }

        foreach (GameObject unit in playerUnits)
        {
            if (Vector3.Distance(unit.transform.position, npc.transform.position) < Vector3.Distance(closestUnitPosition, npc.transform.position)) // find closest unit
            {
                closestUnit = unit;
            }
        }

        if(Vector3.Distance(closestUnit.transform.position, npc.transform.position) > aggroRange && closestUnit != null)
        {
            nextState = new Retreat(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked, origin, closestUnit);
            stage = EVENT.EXIT;
        }
        if(closestUnit == null && npc.GetComponent<Ai>().charge)
        {
            nextState = new Charge(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
            stage = EVENT.EXIT;
        }
        if(closestUnit == null)
        {
            nextState = new Idle(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective, isPatrol, gettingAttacked);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        base.Exit();

    }





}

