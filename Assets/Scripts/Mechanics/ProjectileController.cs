using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] AudioShot impactSound = null;
    [SerializeField] float projectileSpeed = 5;

    bool _rightShot = false;
    int _damage;

    public void SetupProjectile(bool rightShot, int damage) 
    { 
        _rightShot = rightShot;
        _damage = damage;
        Destroy(this.gameObject, 5f);
    }

    void Update()
    {
        if (_rightShot)
        {
            transform.position += transform.right * projectileSpeed * Time.deltaTime;
            return;
        }

        transform.position -= transform.right * projectileSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Wall" || other.tag == "Ground")
        {
            impactSound.Play(this.transform.position);
            Destroy(this.gameObject);
            return;
        }

        if (other.tag == "Enemy")
        {
            impactSound.Play(this.transform.position);
            other.TryGetComponent(out IHealth health);

            if (health != null)
                health.HandleHealth(_damage);


            Destroy(this.gameObject);
            return;
        }
    }
}
