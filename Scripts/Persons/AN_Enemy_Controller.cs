using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AN_Enemy_Controller : MonoBehaviour
{
    public float addRandomSpeed = 2f;

    NavMeshAgent agent;
    Transform target;

    private void Start()
    {
        target = FindObjectOfType<AN_TDS_Controller>().transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed += Random.Range(0f, addRandomSpeed);
    }

    private void Update()
    {
        agent.destination = target.position;
        if (Vector3.Distance(target.position, transform.position) < agent.stoppingDistance)
        {
            Vector3 pos = target.position; pos.y = transform.position.y;
            transform.LookAt(pos, Vector3.up);
        }
    }
}
