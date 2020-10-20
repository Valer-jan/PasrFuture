using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_DamageZone : MonoBehaviour
{
    public bool ImpulseDamage = true;
    public float Damage = 10f, Radius = 5f;

    private void Start()
    {
        if (ImpulseDamage)
        {
            Collider[] checkObj = Physics.OverlapSphere(transform.position, Radius);
            foreach(var obj in checkObj)
            {
                AN_PlayerParams pr = obj.GetComponent<AN_PlayerParams>();
                AN_EnemyBaheviour en = obj.GetComponent<AN_EnemyBaheviour>();

                if (pr != null) pr.CurrentHealth -= Damage;
                if (en != null) en.CurrentHealth -= Damage;
            }
        }
    }

    private void Update()
    {
        if (!ImpulseDamage)
        {
            Collider[] checkObj = Physics.OverlapSphere(transform.position, Radius);
            foreach (var obj in checkObj)
            {
                AN_PlayerParams pr = obj.GetComponent<AN_PlayerParams>();
                AN_EnemyBaheviour en = obj.GetComponent<AN_EnemyBaheviour>();

                if (pr != null) pr.CurrentHealth -= Damage * Time.deltaTime;
                if (en != null) en.CurrentHealth -= Damage * Time.deltaTime;
            }
        }
    }
}
