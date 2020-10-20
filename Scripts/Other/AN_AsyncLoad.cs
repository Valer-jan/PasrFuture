using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AN_AsyncLoad : MonoBehaviour
{
    public Slider slider;
    public GameObject destroy;

    bool triggerOnce = false;

    private void Update()
    {
        if (Input.anyKey && !triggerOnce)
        {
            StartCoroutine(Load());
            destroy.SetActive(false);

            triggerOnce = true;
        }
    }

    IEnumerator Load()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(1);

        while (!op.isDone)
        {
            // float value = Mathf.Clamp01(op.progress / .9f);
            slider.value = op.progress;
            Debug.Log(op.progress);

            yield return null;
        }
    }
}
