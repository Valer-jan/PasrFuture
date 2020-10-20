using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AN_InterfaceLog : MonoBehaviour
{
    public Color[] Colors;
    Text text;

    private void Start()
    {
        text = GetComponent<Text>();
        text.enabled = false;

        StartCoroutine(SendInterfaceMassage(text.text, 5f));
    }

    public IEnumerator SendInterfaceMassage(string massage, float timer)
    {
        if (timer > 10f || timer <= 0f) timer = 10f;
        float currentTimer = timer;

        text.enabled = true;
        text.text = massage;

        int i = 0;
        while(currentTimer > 0)
        {
            if (i > Colors.Length - 1) i = 0;
            text.color = Colors[i];
            i++;

            currentTimer -= .4f;
            yield return new WaitForSeconds(.4f);
        }
        text.enabled = false;
    }
}
