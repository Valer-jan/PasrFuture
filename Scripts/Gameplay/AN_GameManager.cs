using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class AN_GameManager : MonoBehaviour
{
    #region declorations

    public int Score = 0;
    public float RespownStartRate = 2, MinusRate = .05f;
    public int EnemiesLim = 50;
    [Range(0f, 1f)]
    public float CapabilityToGenerate = .1f;

    [Header("Cursor")]
    public Texture2D CursorTexture;
    // public int CursorSize = 100;

    [Header("BonusEffect")]
    public GameObject GetEffect;
    public AudioClip GetClip;
    AudioSource GetSource;

    [Header("RespownEvents")]
    public RespownEventGroup[] Events;

    [Header("Lists and Arrays")]
    public List<Transform> Respowns;
    public List<GameObject> Enemies;
    public List<GameObject> Weapons;
    public AudioClip[] PortalBooms;
    public GameObject[] EnableAfterBoom;

    [Header("Other objects")]
    public Text TextScore;
    public GameObject Explosion;

    [Header("After death")]
    public PostProcessVolume PostProc;
    public GameObject[] ActivateAfter;
    public Text afterScore;
    public bool StartGamePlayTrigger = false, ItsOver = false;

    //[HideInInspector]
    public int ActivatedComputers = 0;
    float CurrentTimeToRespown = 0f;
    public int Respowned = 0, RespownedTrigger;
    AudioSource aud;
    GameObject player;
    ColorGrading grading;

    #endregion

    private void Start()
    {
        GetSource = gameObject.AddComponent<AudioSource>();
        GetSource.loop = false;
        GetSource.playOnAwake = false;
        GetSource.clip = GetClip;

        aud = GetComponent<AudioSource>();
        player = FindObjectOfType<AN_TDS_Controller>().gameObject;

        foreach (var boom in EnableAfterBoom) boom.SetActive(false);
        foreach (var ob in ActivateAfter) ob.SetActive(false);

        // cursor
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware); // new Vector2(CursorTexture.width / 2, CursorTexture.width / 2)

        // grading
        PostProc.profile.TryGetSettings(out grading);
    }

    private void Update()
    {
        // enemy spown
        if (ActivatedComputers == 3)
        {
            CurrentTimeToRespown += Time.deltaTime;
            if (CurrentTimeToRespown > RespownStartRate && Respowned - Score < EnemiesLim)
            {
                CurrentTimeToRespown = 0f;

                // find respown position
                Vector3 pos = Respowns[0].position;
                int lim = 0;
                while (lim < 100)
                {
                    Vector3 checkPos = Respowns[UnityEngine.Random.Range(0, Respowns.Count)].position;
                    if (Vector3.Distance(checkPos, player.transform.position) > 10)
                    {
                        pos = checkPos;
                        lim = 100;
                    }
                    lim++;
                }

                Instantiate(Enemies[UnityEngine.Random.Range(0, Enemies.Count)], pos, Quaternion.identity);
                Respowned++;
                if (RespownStartRate > .2f) RespownStartRate -= MinusRate;
            }

            if (!StartGamePlayTrigger) // trigger once
            {
                StartCoroutine(PortalError());

                StartGamePlayTrigger = true;
            }
        }
        TextScore.text = Score.ToString();

        // respown events
        foreach (var item in Events)
        {
            if (Respowned == item.RespownedCount && RespownedTrigger < Respowned)
            {
                StartCoroutine(StartRespownEvent(item));
                RespownedTrigger = Respowned;
            }
        }

        if (ItsOver && grading.saturation.value > -95f)
        {
            grading.saturation.value -= Time.deltaTime * 10;
        }
    }

    public void GanerateWeapon(Transform pos)
    {
        float random = UnityEngine.Random.Range(0f, 1f);
        Vector3 instPos = pos.position;
        instPos.y = player.transform.position.y;
        if (random < CapabilityToGenerate) Instantiate(Weapons[UnityEngine.Random.Range(0, Weapons.Count)], instPos, Quaternion.identity);
    }

    public void FinishMethod()
    {
        foreach(var ob in ActivateAfter)
        {
            ob.SetActive(true);
        }
        afterScore.text = Score.ToString();
        ItsOver = true;
    }

    IEnumerator PortalError()
    {
        for (int i = 0; i < EnableAfterBoom.Length; i++)
        {
            AudioBoom();

            EnableAfterBoom[i].SetActive(true);
            Instantiate(Explosion, EnableAfterBoom[i].transform.position, Quaternion.identity);

            StartCoroutine(FindObjectOfType<AN_cameraShake>().Shake(.2f, .2f));

            yield return new WaitForSeconds(UnityEngine.Random.Range(.3f, .9f));
        }
    }

    void AudioBoom()
    {
        aud.clip = PortalBooms[UnityEngine.Random.Range(0, PortalBooms.Length)];
        aud.pitch = UnityEngine.Random.Range(.9f, 1.1f);
        aud.Play();
    }

    IEnumerator StartRespownEvent(RespownEventGroup EventGroup)
    {
        StartCoroutine(FindObjectOfType<AN_InterfaceLog>().SendInterfaceMassage(EventGroup.InterfaceMassage, 4f)); // interface massage

        foreach (var newResp in EventGroup.NewRespownes)
        {
            Respowns.Add(newResp);
        }
        foreach (var add in EventGroup.NewWeapons)
        {
            Weapons.Add(add);
        }
        foreach (var add in EventGroup.NewEnemies)
        {
            Enemies.Add(add);
        }

        foreach (var item in EventGroup.DestroyObjects)
        {
            Instantiate(Explosion, item.transform.position, Quaternion.identity);
            StartCoroutine(FindObjectOfType<AN_cameraShake>().Shake(.2f, .2f));
            AudioBoom();
            Destroy(item, .1f);

            yield return new WaitForSeconds(UnityEngine.Random.Range(.2f, .5f));
        }
    }

    public void GetEffectVoid()
    {
        GetSource.Play();
        Instantiate(GetEffect, FindObjectOfType<AN_TDS_Controller>().transform.position, Quaternion.identity);
    }
}


[Serializable]
public class RespownEventGroup
{
    public string Name;
    public int RespownedCount;
    public string InterfaceMassage;
    public Transform[] NewRespownes;
    public GameObject[] NewWeapons, NewEnemies, DestroyObjects;
}