using UnityEngine;

public class AN_Bonus : MonoBehaviour
{
    public float BonusTimer = 10f, SpeedBonus = 2f;

    SphereCollider sp;
    Rigidbody rb;

    private void Start()
    {
        sp = gameObject.AddComponent<SphereCollider>();
        sp.radius = .5f;
        sp.isTrigger = true;

        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Update()
    {
        transform.Rotate(0f, 100f * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        AN_TDS_Controller tds = other.GetComponent<AN_TDS_Controller>();
        if (tds != null)
        {
            tds.SpeedBonusTimer = BonusTimer;
            tds.BonusSpeed = SpeedBonus;

            FindObjectOfType<AN_GameManager>().GetEffectVoid(); // bonus effect
            GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }
    }
}
