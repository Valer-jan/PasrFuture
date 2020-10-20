using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_MainCamera : MonoBehaviour
{
    public Transform Character;
    public Vector3 Offset;
    public float LerpPos = .5f;

    private void Start()
    {
        Vector3 target = Character.position + Offset;
        transform.LookAt(Character);
    }

    void Update()
    {
        Vector3 target = Character.position + Offset;

        // float dis = Vector3.Distance(Character.position, transform.position);
        transform.position = Vector3.Lerp(transform.position, target, LerpPos);
        // transform.position = target;
    }
}
