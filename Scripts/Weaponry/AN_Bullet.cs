using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Rigidbody))]
public class AN_Bullet : MonoBehaviour
{
    public bool EnemyBullet = false;
    public float Damage = 1f, Speed = 10f;
    public GameObject Impact, Blood;

    private void Start()
    {
        AudioSource aud = GetComponent<AudioSource>();
        if (aud != null) aud.pitch = Random.Range(.9f, 1.1f);

        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        AN_EnemyBaheviour en = collision.gameObject.GetComponent<AN_EnemyBaheviour>();
        AN_Bullet bullet = collision.gameObject.GetComponent<AN_Bullet>();

        if (bullet == null) // for shortgun
        {
            if (!EnemyBullet)
            {
                if (en != null)
                {
                    en.CurrentHealth -= Damage;
                    Instantiate(Blood, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
            else // enemy bullet
            {
                AN_PlayerParams pr = collision.gameObject.GetComponent<AN_PlayerParams>();
                if (pr != null)
                {
                    pr.CurrentHealth -= Damage;
                    Instantiate(Blood, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else if (en == null) Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        if (Impact != null) Instantiate(Impact, transform.position, transform.rotation);
    }
}
