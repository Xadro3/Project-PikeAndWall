using System.Collections;
using UnityEngine;

public class UnitClass : MonoBehaviour
{
    AudioSource audio;
    [SerializeField] public float firerate;
    [SerializeField] public float range;
    public Transform weapon;
    public Hitbox targetHitbox;
    public Health targetHealth;
    private UnitAttack unitAttack;
    public int damageValue = 3;
    public bool enemyInRange;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        unitAttack = weapon.GetComponentInChildren<UnitAttack>();
    }

    void Update()
    {
    }

    public Health SetTarget(Hitbox targetHitbox)
    {
        this.targetHitbox = targetHitbox;
        targetHealth = targetHitbox.GetComponentInParent<Health>();
        StartAttack();
        return targetHealth;
    }

    public void StartAttack(){
        unitAttack.StartAttack();
    }

}
