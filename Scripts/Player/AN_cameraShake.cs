using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_cameraShake : MonoBehaviour
{
    public bool TestButton = false;
    public float duration, magnitude;

    private void Update()
    {
        if (TestButton)
        {
            StartCoroutine(Shake(duration, magnitude));
            TestButton = false;
        }
    }

    public IEnumerator Shake (float duration, float magnitude)
    {
        float elapsed = 0;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = Vector3.zero;
    }
}
