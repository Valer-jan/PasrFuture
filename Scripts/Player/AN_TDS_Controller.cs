using System.Collections.Generic;
using UnityEngine;

public class AN_TDS_Controller : MonoBehaviour
{
    public int MoveSpeed = 3;

    [Header("Runing")]
    public int ShiftAddSpeed = 2;
    public float MaxEndurance = 100f, IncreaseEndurance = 5f, DecreaseEndurance = 10f;
    public ParticleSystem RunEffect;

    [HideInInspector]
    public float SpeedBonusTimer = 0f;
    float currentEndurance;
    CharacterController CharMove;
    float vrtcl = -2f;
    public float BonusSpeed = 2;

    private void Start()
    {
        CharMove = GetComponent<CharacterController>();

        currentEndurance = MaxEndurance;
    }

    private void Update()
    {
        if (vrtcl > -2f) vrtcl -= Time.deltaTime * 5;
        // moving
        Vector3 axeBackward = Vector3.Cross(Vector3.up, Camera.main.transform.right);
        Vector3 move = Camera.main.transform.right * Input.GetAxis("Horizontal")
            - axeBackward * Input.GetAxis("Vertical")
            + vrtcl * Vector3.up;

        float FinalSpeed = MoveSpeed;

        // runing
        if (Input.GetButton("Fire3") && currentEndurance > 0)
        {
            FinalSpeed += ShiftAddSpeed;
            if (RunEffect.isStopped) RunEffect.Play();
            currentEndurance -= DecreaseEndurance * Time.deltaTime;
        }
        else
        {
            if (RunEffect.isPlaying) RunEffect.Stop();
        }
        if (!Input.GetButton("Fire3") && currentEndurance < MaxEndurance) currentEndurance += Time.deltaTime * IncreaseEndurance;

        // bonus speed
        if (SpeedBonusTimer > 0)
        {
            FinalSpeed += BonusSpeed;
            SpeedBonusTimer -= Time.deltaTime;
        }

        CharMove.Move(move * Time.deltaTime * FinalSpeed);

        // rotation to cursor
        Ray RayMoving = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane PlaneMoving = new Plane(Vector3.up, transform.position); // transform.position.y
        float DistanceToPlane;

        Vector3 LookPos = transform.position;
        if (PlaneMoving.Raycast(RayMoving, out DistanceToPlane)) LookPos = RayMoving.GetPoint(DistanceToPlane);
        LookPos.y = transform.position.y;
        transform.LookAt(LookPos);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (Input.GetButtonDown("Jump") && collision != null) vrtcl = 2f;
    }
}
