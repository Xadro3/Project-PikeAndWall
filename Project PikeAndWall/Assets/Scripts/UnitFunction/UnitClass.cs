using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnitClass : MonoBehaviour
{
    AudioSource audio;
    [Header ("Weapon Stats")]
    [Range(0,3)]
    [SerializeField] public float fireRate;
    [Range(5,60)]
    [SerializeField] public float range;
    [Range(1,10)]
    [SerializeField] public int damageValue;
    [Range(0,5)]
    [SerializeField] public float turnRate;
    [Header ("Functional stuff")]
    public Transform weapon;
    public Hitbox targetHitbox;
    public Health targetHealth;
    public bool enemyInRange;
    public UnitAttack unitAttack;
    public float buildTime;
    public string className;
    public TargetHandler targetHandler;
    [Header ("Animation")]
    public Animator animatorWeapon;
    public Animator animatorRig;
    
    private UnitReach unitReach;
    private float rangeOld;
    
    
    public List<ResourceValue> upgradeCost;
    public List<ResourceValue> buildCost;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        unitReach = GetComponentInChildren<UnitReach>();
        unitAttack = weapon.GetComponentInChildren<UnitAttack>();
        targetHandler = GetComponent<TargetHandler>();
        damageValue = 0;

        SetWeaponStats();
    }
    
    void Start()
    {
        rangeOld = range;
    }

    void Update()
    {
        if (rangeOld != range)
        {
            range = unitReach.SetUnitReach(range);
            rangeOld = range;
        }
    }
    
    public void SetTarget(Hitbox targetHitbox)
    {
        if (targetHitbox != null)
        {
            SetWeaponStats();
            this.targetHitbox = targetHitbox;
            targetHealth = targetHitbox.GetComponentInParent<Health>();
            SetDamage();
            //unitAttack.StartAttack();
        }
    }

    public void SetDamage()
    {
        if ((className == "Archer" || className == "Musket")&&(targetHitbox.GetComponentInParent<UnitClass>().className == "Pikeman" || targetHitbox.GetComponentInParent<UnitClass>().className == "HeavyPikeman"))
        {
            damageValue += 1;
        }
        if ((className == "Pikeman" || className == "HeavyPikeman")&&(targetHitbox.GetComponentInParent<UnitClass>().className == "Cavalry" || targetHitbox.GetComponentInParent<UnitClass>().className == "HeavyCavalry"))
        {
            damageValue += 1;
        }
        if ((className == "Cavalry" || className == "HeavyCavalry")&&(targetHitbox.GetComponentInParent<UnitClass>().className == "Archer" || targetHitbox.GetComponentInParent<UnitClass>().className == "Musket"))
        {
            damageValue += 1;
        }
        if(targetHitbox.GetComponentInParent<UnitClass>().className == "Objective")
        {
            damageValue += 1;
        }
        //activate for trailer stuff
        //damageValue = 0;
    }

    public void PlayAnimation(string trigger)
    {
        if (animatorWeapon != null)
        {
            animatorWeapon.SetTrigger(trigger);
        }
        if (animatorRig != null)
        {
            animatorRig.SetTrigger(trigger);
        }
        
    }

    private void SetWeaponStats()
    {
        if (className == "Objective")
        {
            fireRate = 0f;
            range = 0f;
            damageValue = 0;
            turnRate = 0f;
        }
        else if (unitAttack.gameObject.name == "Sword")
        {
            fireRate = 1f;
            range = 7.5f;
            damageValue = 1;
            turnRate = 2f;
        }
        else if (unitAttack.gameObject.name == "Spear")
        {
            fireRate = 1f;
            range = 7.5f;
            damageValue = 1;
            turnRate = 2f;
        }
        else if (unitAttack.gameObject.name == "HeavySpear")
        {
            fireRate = 1f;
            range = 7.5f;
            damageValue = 2;
            turnRate = 2f;
        }
        else if (unitAttack.gameObject.name == "Bow")
        {
            fireRate = 1f;
            range = 50f;
            damageValue = 1;
            turnRate = 2f;
        }        
        else if (unitAttack.gameObject.name == "Musket")
        {
            fireRate = 1f;
            range = 40f;
            damageValue = 2;
            turnRate = 2f;
        }
        else if (unitAttack.gameObject.name == "Pistol")
        {
            fireRate = 1f;
            range = 15f;
            damageValue = 2;
            turnRate = 2f;
        }

    }
}