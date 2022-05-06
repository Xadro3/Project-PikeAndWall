using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class States 
{
   
    public enum STATE
    {
        IDLE, ATTACK, PURSUIT, RETREAT, PATROL, CHARGE
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
    protected List<Transform> playerUnits; // all Player Units
    protected States nextState;
    protected NavMeshAgent agent;
    protected Transform objective; //position to charge to
    protected float aggroRange = 50f;
    protected float attackRange;
    protected bool isGuard; //prevent pursuit
    protected bool charge;
    public States(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<Transform> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
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
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _anim, List<Transform> _playerUnits, float _attackRange, bool _isGuard, bool _charge, Transform _objective)
                : base(_npc, _agent, _anim, _playerUnits, _attackRange, _charge, _charge, _objective)
    {
        name = STATE.IDLE;
    }
    public override void Enter()
    {
        animator.SetTrigger("isIdle");

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

            foreach (Transform unit in playerUnits)
            {
                if ((Vector3.Distance(unit.position, npcPos)) < aggroRange)
                {
                    nextState = new Pursue(npc, agent, animator, unit, playerUnits, npcPos);
                    stage = EVENT.EXIT;
                }
            }
            if (Random.Range(0, 1000) < 10)
            {
                nextState = new Patrol;
                stage = EVENT.EXIT;
            }
            if (charge)
            {
                nextState = new Charge();
                    stage = EVENT.EXIT;
            }
        }
        
        base.Update();
    }

    public override void Exit()
    {
        animator.ResetTrigger("isIdle");
        base.Exit();
    }
}
