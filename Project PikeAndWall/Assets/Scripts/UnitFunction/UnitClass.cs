using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private UnitReach unitReach;
    private float rangeOld;
    public float buildTime;
    public TargetHandler targetHandler;
    public string className;
    public Animator animatior;

    public List<ResourceValue> upgradeCost;
    public List<ResourceValue> buildCost;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        unitReach = GetComponentInChildren<UnitReach>();
        unitAttack = weapon.GetComponentInChildren<UnitAttack>();
        targetHandler = GetComponent<TargetHandler>();
        animatior = GetComponent<Animator>();
        
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
            this.targetHitbox = targetHitbox;
            targetHealth = targetHitbox.GetComponentInParent<Health>();
            //unitAttack.StartAttack();
        }
    }

    private void SetWeaponStats()
    {
        if (unitAttack.gameObject.name == "Sword")
        {
            fireRate = 1f;
            range = 2.5f;
            damageValue = 1;
            turnRate = 2f;
        }
        if (unitAttack.gameObject.name == "Spear")
        {
            fireRate = 1f;
            range = 5f;
            damageValue = 1;
            turnRate = 2f;
        }
        if (unitAttack.gameObject.name == "HeavySpear")
        {
            fireRate = 1f;
            range = 5f;
            damageValue = 2;
            turnRate = 2f;
        }
        if (unitAttack.gameObject.name == "Bow")
        {
            fireRate = 1f;
            range = 50f;
            damageValue = 1;
            turnRate = 2f;
        }        
        if (unitAttack.gameObject.name == "Musket")
        {
            fireRate = 1f;
            range = 40f;
            damageValue = 2;
            turnRate = 2f;
        }
        if (unitAttack.gameObject.name == "Pistol")
        {
            fireRate = 1f;
            range = 10f;
            damageValue = 2;
            turnRate = 2f;
        }
    }


}
