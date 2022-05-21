using System.Collections;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    AudioSource audio;
    [Header ("Weapon Stats")]
    [Range(1,10)]
    [SerializeField] public float fireRate;
    [Range(5,50)]
    [SerializeField] public float range;
    [Range(1,10)]
    [SerializeField] public int damageValue;
    [Range(1,10)]
    [SerializeField] public float turnRate;
    [Header ("Functional stuff")]
    public Transform weapon;
    public Hitbox targetHitbox;
    public Health targetHealth;
    public bool enemyInRange;
    private UnitAttack unitAttack;
    private UnitReach unitReach;
    private float rangeOld;
    public float buildTime;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
        unitReach = GetComponentInChildren<UnitReach>();
        unitAttack = weapon.GetComponentInChildren<UnitAttack>();
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
        this.targetHitbox = targetHitbox;
        targetHealth = targetHitbox.GetComponentInParent<Health>();
        unitAttack.StartAttack();
    }
    private void SetWeaponStats()
    {
        if (unitAttack.gameObject.name == "Sword")
        {
            fireRate = 4;
            range = 5;
            damageValue = 7;
            turnRate = 1;
        }
        if (unitAttack.gameObject.name == "Spear")
        {
            fireRate = 3;
            range = 10;
            damageValue = 10;
            turnRate = 3;
        }
        if (unitAttack.gameObject.name == "Bow")
        {
            fireRate = 2;
            range = 30;
            damageValue = 6;
            turnRate = 2;
        }        
        if (unitAttack.gameObject.name == "Musket")
        {
            fireRate = 1;
            range = 50;
            damageValue = 10;
            turnRate = 2;
        }
    }

}
