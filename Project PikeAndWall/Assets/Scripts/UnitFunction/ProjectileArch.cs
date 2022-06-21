using UnityEngine;
using System.Collections;

public class ProjectileArch : MonoBehaviour
{
    private float speed = 1f;
    public Transform sunrise;
    public Transform sunset;
    public Vector3 center;
    public Vector3 riseRelCenter;
    public Vector3 setRelCenter;
    public Vector3 relCenter;
    public float journeyTime = 2f;
    private float startTime;
    private int damageValue;
    private Quaternion lookRotation;
    private Vector3 lookDirection;

    void Awake()
    {
        startTime = Time.time;
        StartCoroutine(TimeToLive());

    }
    
    
    
    void Start()
    {
        StartCoroutine(TimeToLive());
    }

    private IEnumerator TimeToLive()
    {
        Destroy(gameObject, 3f);
        yield break;
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Hitbox hitbox))
        {
            if (collision.CompareTag("Enemy") && gameObject.CompareTag("Player") || collision.CompareTag("Player") && gameObject.CompareTag("Enemy"))
            {
                Health hit = hitbox.GetComponentInParent<Health>();
                hit.TakeDamage(damageValue);
                Destroy(gameObject,0f);
            }
        }
    }

    public void SetProjectileArch(int damageValue, Transform sunrise, Transform sunset)
    {
        
        this.sunrise = sunrise;
        this.sunset = sunset;
        center = (sunrise.position + sunset.position) * 0.5f;
        center -= new Vector3(0, 4, 0);
        riseRelCenter = sunrise.position - center;
        setRelCenter = sunset.position - center;
        relCenter = center + new Vector3(-100, -100, 0);
        transform.eulerAngles = relCenter;


        SetDamage(damageValue);
    }
    public void SetDamage(int damageValue)
    {
        this.damageValue = damageValue;
    }
    
    private void RotateTowardsEnemy()
    {
        lookDirection = (sunset.position - transform.position).normalized;
        lookRotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    private void Update()
    {
        float fracComplete = (Time.time - startTime) / journeyTime * speed;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete * speed);
        transform.position += center;
        RotateTowardsEnemy();
    }
}
