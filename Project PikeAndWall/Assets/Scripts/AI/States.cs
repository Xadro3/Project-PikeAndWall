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
    public States(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
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
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard,_charge, _objective)
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

        if (isGuard)    //if the unit is guard it will only attack within its range and not pursue
        {

        }

        if (!isGuard) //if the unit us not guard it will pursue units inside its aggro range and engage them, they can also randomly start to patrol and if a charge
                      //flag is set they will move to the position of the objective and attack units in their range
        {
            Vector3 npcPos = npc.transform.position;
           
            foreach (GameObject unit in playerUnits)
            {
                if ((Vector3.Distance(unit.transform.position, npcPos)) < aggroRange)
                {
                   nextState = new Pursue(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective);
                   Debug.Log("AI transitioning to Pursue");
                   stage = EVENT.EXIT;
                }
            }
            if (Random.Range(0, 1000) < 10)
            {
                nextState = new Patrol(npc,agent,animator,playerUnits, attackRange, isGuard, charge, objective);
                Debug.Log("AI is transitioning to Patrol");
                stage = EVENT.EXIT;
            }
            if (charge)
            {
                nextState = new Charge(npc, agent, animator, playerUnits, attackRange, isGuard, charge, objective);
                Debug.Log("Ai is Transitioning to Charge");
                    stage = EVENT.EXIT;
            }
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
    public Pursue(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard, _charge, _objective)
    {
        Debug.Log("AI is in Pursue");
    }
}

public class Patrol: States
{
    int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard,_charge, _objective)
    {
        name = STATE.PATROL;
        agent.speed = 3.5f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        Debug.Log("AI is in Patrol");
        currentIndex = 0;
        animator.SetTrigger("isWalking");
        base.Enter();
    }


    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if(currentIndex >= GameEnviroment.Singleton.Perimeter.Count-1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            agent.SetDestination(GameEnviroment.Singleton.Perimeter[currentIndex].transform.position);
        }

        
    }


    public override void Exit()
    {
        animator.ResetTrigger("isWalking");
        base.Exit();
    }

}
public class Charge: States
{
    public Charge(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<GameObject> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _isGuard, _charge, _objective)
    {

    }
}
