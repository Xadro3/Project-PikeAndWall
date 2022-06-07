using UnityEngine;

public class ProjectileArch : MonoBehaviour
{
    private float speed = 1f;
    public Transform sunrise;
    public Transform sunset;
    public Vector3 center;
    public Vector3 riseRelCenter;
    public Vector3 setRelCenter;
    public float journeyTime = 2f;
    private float startTime;
    private int damageValue;

    void Awake()
    {
        startTime = Time.time;
        Destroy(gameObject, 4f);
    }
    
    void Start()
    {
        
        
        //sunrise = unit.weapon.transform;
        //sunset = unit.targetHitbox.transform;
        //center = (sunrise.position + sunset.position) * 0.5f;
        //center -= new Vector3(0, 1, 0);
        //riseRelCenter = sunrise.position - center;
        //setRelCenter = sunset.position - center;
       

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out Hitbox hitbox))
        {
            if (collision.CompareTag("Enemy") && gameObject.CompareTag("Player") || collision.CompareTag("Player") && gameObject.CompareTag("Enemy"))
            {
                Health hit = hitbox.GetComponentInParent<Health>();
                hit.TakeDamage(damageValue);
                Destroy(gameObject, 0f);
            }
        }
        
        //if (collision.TryGetComponent<Hitbox>(out Hitbox hitbox))
        //{
        //    Health hit = hitbox.GetComponentInParent<Health>();
        //    hit.TakeDamage(damageValue);
        //    Destroy(gameObject, 0f);
        //}
    }

    public void SetProjectileArch(int damageValue, Transform sunrise, Transform sunset)
    {
        
        this.sunrise = sunrise;
        this.sunset = sunset;
        center = (sunrise.position + sunset.position) * 0.5f;
        center -= new Vector3(0, 1, 0);
        riseRelCenter = sunrise.position - center;
        setRelCenter = sunset.position - center;
        
        SetDamage(damageValue);
    }
    public void SetDamage(int damageValue)
    {
        this.damageValue = damageValue;
    }

    private void Update()
    {

        float fracComplete = (Time.time - startTime) / journeyTime * speed;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete * speed);
        transform.position += center;

    }
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }
}
