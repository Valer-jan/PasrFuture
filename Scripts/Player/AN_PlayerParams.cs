using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AN_PlayerParams : MonoBehaviour
{
    public Image RedImage;
    public float MaxHealth = 100f, CurrentHealth = 100f;
    public float Regen = 1f;

    Color clr = new Color(1, 1, 1, 1);
    bool OverTrigger = false;

    private void Update()
    {
        if (CurrentHealth > 0 && CurrentHealth < MaxHealth) CurrentHealth += Time.deltaTime * Regen;
        clr.a = 1 - CurrentHealth / MaxHealth;
        RedImage.color = clr;

        if (!OverTrigger && CurrentHealth <= 0)
        {
            OverTrigger = true;

            GetComponent<AN_TDS_Controller>().enabled = false;
            FindObjectOfType<AN_GameManager>().FinishMethod();
        }
    }
}
