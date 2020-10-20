using UnityEngine;

public class AN_Granate : MonoBehaviour
{
    public bool EnableTimer = true;
    public float DropForce = 200f;
    public GameObject ImpactObject;
    public Vector3 DropRot;
    [Space]
    public float TimerDestroy = 20f, currentTimer = 0f;

    bool Selected = false, Droped = false, DropTrigger = false;
    Transform Belt, DropPos;
    AN_PlayerWeapon pweapon;
    AN_GameManager manager;
    Rigidbody rb;
    Light lgt;

    private void Start()
    {
        pweapon = FindObjectOfType<AN_PlayerWeapon>();
        manager = FindObjectOfType<AN_GameManager>();
        Belt = pweapon.Belt;
        DropPos = pweapon.DropPosition;

        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        lgt = GetComponent<Light>();
    }

    private void Update()
    {
        if (Selected)
        {
            if (lgt.enabled) lgt.enabled = false;

            if (Droped)
            {
                if (!DropTrigger)
                {
                    transform.position = DropPos.position;
                    transform.rotation = DropPos.rotation;
                    transform.Rotate(DropRot);

                    rb.isKinematic = false;
                    rb.AddForce(transform.forward * DropForce, ForceMode.Impulse);

                    DropTrigger = true;
                }
            }
            else
            {
                transform.position = Belt.position;
                transform.rotation = Belt.rotation;

                if (Input.GetButtonDown("Fire2") && !manager.ItsOver && !Input.GetButton("Fire3"))
                {
                    Droped = true;
                    pweapon.BonusEquiped = false;
                }
            }
        }
        else
        {
            if (!lgt.enabled) lgt.enabled = true;
            if (currentTimer > TimerDestroy) Destroy(gameObject);

            if (EnableTimer) currentTimer += Time.deltaTime;
            transform.Rotate(0f, 100f * Time.deltaTime, 0f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AN_PlayerWeapon>() != null && !pweapon.BonusEquiped && !Droped)
        {
            Selected = true;
            pweapon.BonusEquiped = true;

            FindObjectOfType<AN_GameManager>().GetEffectVoid(); // bonus effect
        }
        else if (Droped)
        {
            Instantiate(ImpactObject, transform.position, Quaternion.identity);
            FindObjectOfType<AN_cameraShake>().StartCoroutine(FindObjectOfType<AN_cameraShake>().Shake(.2f, .2f));
            Destroy(gameObject);
        }
    }
}
