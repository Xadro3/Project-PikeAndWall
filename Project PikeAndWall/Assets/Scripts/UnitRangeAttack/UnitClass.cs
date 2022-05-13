using System.Collections;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    AudioSource audio;
    [Header ("Weapon Stats")]
    [Range(1,10)]
    [SerializeField] public float firerate;
    [Range(5,50)]
    [SerializeField] public float range;
    [Range(1,10)]
    [SerializeField] public int damageValue;
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
        StartAttack();
    }
    public void StartAttack(){
        unitAttack.StartAttack();
    }
    private void SetWeaponStats()
    {
        if (unitAttack.gameObject.name == "Sword")
        {
            firerate = 4;
            range = 5;
            damageValue = 7;
        }
        if (unitAttack.gameObject.name == "Spear")
        {
            firerate = 3;
            range = 10;
            damageValue = 10;
        }
        if (unitAttack.gameObject.name == "Bow")
        {
            firerate = 2;
            range = 30;
            damageValue = 6;
        }        
        if (unitAttack.gameObject.name == "Musket")
        {
            firerate = 1;
            range = 50;
            damageValue = 10;
        }
    }

}
