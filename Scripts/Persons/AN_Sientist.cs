using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AN_Sientist : MonoBehaviour
{
    public GameObject DeathEffect;
    public Animator anim;

    bool GOtrigger = false;
    AN_GameManager manager;
    NavMeshAgent agent;
    AN_TDS_Controller tds;

    private void Start()
    {
        manager = FindObjectOfType<AN_GameManager>();
        agent = GetComponent<NavMeshAgent>();
        tds = FindObjectOfType<AN_TDS_Controller>();
    }

    private void Update()
    {
        if (!manager.StartGamePlayTrigger)
        {
            Vector3 look = tds.transform.position;
            look.y = transform.position.y;
            transform.LookAt(look);
        }
        else
        {
            if(!GOtrigger)
            {
                GOtrigger = true;
                anim.SetTrigger("go");
                StartCoroutine(GoAway());
            }
        }
    }

    IEnumerator GoAway()
    {
        int lim = 100, i = 0;
        while(i < lim)
        {
            agent.destination = new Vector3(Random.Range(-10f, 10f), 1, Random.Range(-40f, 40f));
            yield return new WaitForSeconds(Random.Range(1f, 5f));
        }
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Instantiate(DeathEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<AN_Bullet>() != null) Destroy(gameObject);
    }
}
