using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AN_ScenaryObject : MonoBehaviour
{
    bool Activated = false, ClickTrigger = false;
    public GameObject ifActive, Screen;
    public Color HiLightColor;

    Light lgt;

    private void Start()
    {
        lgt = gameObject.AddComponent<Light>();
        lgt.color = HiLightColor;
        lgt.intensity = 3;
        lgt.enabled = false;

        Screen.SetActive(false);
    }

    private void OnMouseDown()
    {
        float dis = Vector3.Distance(transform.position, FindObjectOfType<AN_TDS_Controller>().transform.position);
        if (!ClickTrigger && dis < 5f)
        {
            Destroy(ifActive);
            FindObjectOfType<AN_GameManager>().ActivatedComputers++;

            AudioSource aud = GetComponent<AudioSource>();
            if (aud != null) aud.Play();

            Screen.SetActive(true);
            lgt.enabled = false;

            ClickTrigger = true;
        }
    }

    private void OnMouseEnter()
    {
        if (!ClickTrigger)
        {
            lgt.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        lgt.enabled = false;
    }
}
