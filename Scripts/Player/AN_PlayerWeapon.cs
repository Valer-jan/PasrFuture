using UnityEngine;

public class AN_PlayerWeapon : MonoBehaviour
{
    [Header("Weapon")]
    public bool AimingToCursor = false;
    public AN_Weapon WeaponInHands = null;
    public Transform WeaponPoint, PistolSpown;
    public GameObject PistolMesh, PistolBullet;

    [Header("Bonus")]
    public bool BonusEquiped = false;
    public Transform Belt;
    public Transform DropPosition;

    AN_GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<AN_GameManager>();
    }

    private void Update()
    {
        if (WeaponInHands == null && manager.ActivatedComputers == 3 && !manager.ItsOver && Time.timeScale > 0f)
        {
            if (AimingToCursor)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100)) PistolMesh.transform.LookAt(hit.point);
            }

            PistolMesh.SetActive(true);
            if (Input.GetButtonDown("Fire1") && !Input.GetButton("Fire3")) Instantiate(PistolBullet, PistolSpown.position, PistolSpown.rotation);
        }
        else
        {
            PistolMesh.SetActive(false);
        }
    }
}
