using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AN_EnemyBaheviour : MonoBehaviour
{
    public float MaxHealth = 10f, CurrentHealth = 10f, Damage = 1;
    public Animator anim;
    public GameObject DeathEffect;

    [Header("Long Range")]
    //public float Rate = .5f;
    public GameObject Bullet;
    public int Magazine = 1;
    public Transform SpownPoint;

    AN_PlayerParams player;
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<AN_PlayerParams>();

        // size
        AN_GameManager man = FindObjectOfType<AN_GameManager>();
        float size = 1f + man.Respowned * .01f;
        size = Mathf.Clamp(size, 1f, 2f);
        transform.localScale = Vector3.one * Random.Range(1f, size);
        CurrentHealth *= size * 1.5f;

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < agent.stoppingDistance)
        {
            anim.SetBool("atack", true);
            SpownPoint.LookAt(player.transform.position);
        }
        else
        {
            anim.SetBool("atack", false);
            // currentRate = 0f;
        }

        if (CurrentHealth <= 0f) Destroy(gameObject); // ragdoll
    }

    private void OnDestroy()
    {
        AN_GameManager man = FindObjectOfType<AN_GameManager>();
        man.Score++;
        man.GanerateWeapon(transform);
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }

    public void AnimAtack()
    {
        if (Bullet == null) player.CurrentHealth -= Damage;
        else
        {
                SpownPoint.LookAt(player.transform.position);
                StartCoroutine(BulletMagazine(Magazine));
        }
    }

    IEnumerator BulletMagazine(int magazin)
    {
        for (int i = 0; i < magazin; i++)
        {
            Instantiate(Bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(.2f);
        }
    }
}