using System.Collections;
using UnityEngine;

public class AN_Weapon : MonoBehaviour
{
    public bool EnableTimer = true, AimingToCursor = false;
    public int Bullets = 10;
    public float Rate = .5f;
    public float Accuracy = 1f;
    public int BulletPerShort = 1;
    public int timerDestroy = 20;
    [Space]
    public GameObject Bullet, Gunfire;
    public Transform SpownPoint;
    public float CamDuration = .05f, CamMagnitude = .1f;

    float timeDes = 0f; // timer to destroy
    float CurrentRate = 0f;
    bool Selected = false; bool SelectTrigger = false;
    SphereCollider SpTrigger;
    float CurrGetingTimer = 0, GettingTimer = .5f;
    Rigidbody rb;
    Transform Hands;
    AN_GameManager manager;
    Light selectLight;
    AN_cameraShake cameraShake;

    private void Start()
    {
        Hands = FindObjectOfType<AN_PlayerWeapon>().WeaponPoint;

        SpTrigger = gameObject.AddComponent<SphereCollider>();
        SpTrigger = GetComponent<SphereCollider>();
        SpTrigger.radius = 1f;
        SpTrigger.isTrigger = true;

        rb = gameObject.AddComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        manager = FindObjectOfType<AN_GameManager>();

        selectLight = gameObject.AddComponent<Light>();
        selectLight.color = new UnityEngine.Color(1, 1, .5f, 1);
        selectLight.intensity = 3;
        selectLight.range = 5;

        cameraShake = FindObjectOfType<AN_cameraShake>();
    }

    private void Update()
    {
        Selecting();
        if (!manager.ItsOver) Firing();

        if (Bullets <= 0) Destroy(gameObject);

        if (timeDes > timerDestroy) Destroy(gameObject);
    }

    void Selecting()
    {
        if (Selected) 
        {
            transform.position = Hands.position;
            // transform.rotation = Hands.rotation;

            if (AimingToCursor)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100)) transform.LookAt(hit.point);
            }
            else
            {
                transform.rotation = Hands.rotation;
            }

            if (!SelectTrigger) // подбирание с пола единожды
            {
                FindObjectOfType<AN_GameManager>().GetEffectVoid(); // bonus effect

                CurrGetingTimer = 0f;
                SelectTrigger = true;
                selectLight.enabled = false;

                SpTrigger.enabled = false;
                EnableTimer = true;

                timeDes = 0f;
            }
        }
        else
        {
            if (CurrGetingTimer < GettingTimer) CurrGetingTimer += Time.deltaTime; // таймер неподбирания
            transform.Rotate(Vector3.up, Time.deltaTime * 100);

            if (SelectTrigger) // выкидывание на пол единожды
            {
                SelectTrigger = false;
                selectLight.enabled = true;

                SpTrigger.enabled = true;
                // выкинуть из рук
            }

            if (EnableTimer) timeDes += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AN_PlayerWeapon PWeapon = other.GetComponent<AN_PlayerWeapon>();

        if (PWeapon != null && CurrGetingTimer >= GettingTimer && manager.ActivatedComputers == 3)
        {
            if (PWeapon.WeaponInHands != null) PWeapon.WeaponInHands.Selected = false;
            PWeapon.WeaponInHands = this;
            Selected = true;
        }
    }

    void Firing()
    {
        if (CurrentRate < Rate) CurrentRate += Time.deltaTime;
        if (Input.GetButton("Fire1") && Selected && CurrentRate >= Rate && Time.timeScale > 0 && !Input.GetButton("Fire3"))
        {
            StartCoroutine(Fire());
            CurrentRate = 0f;
        }
    }

    IEnumerator Fire()
    {
        StartCoroutine(cameraShake.Shake(CamDuration, CamMagnitude));
        Gunfire.transform.localScale = new Vector3(1, 1, Random.Range(.5f, 1.5f));
        if (Gunfire != null) Instantiate(Gunfire, SpownPoint.position, SpownPoint.rotation);
        Bullets--;

        for (int i = 0; i < BulletPerShort; i++)
        {
            Vector3 quat = SpownPoint.eulerAngles;
            quat.y = quat.y - Accuracy + Random.Range(0f, 2 * Accuracy);
            Instantiate(Bullet, SpownPoint.position, Quaternion.Euler(quat));
            yield return new WaitForSeconds(.01f);
        }
    }
}
