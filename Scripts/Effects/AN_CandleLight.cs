using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_CandleLight : MonoBehaviour
{
    public Color clr;
    public Vector2 LimIntensity = new Vector2(1f, 2f);
    public int SpeedChange = 20;

    bool up = true;
    Light lgt;

    private void Start()
    {
        lgt = gameObject.AddComponent<Light>();
        lgt.color = clr;
        lgt.intensity = LimIntensity.x;
        lgt.range = 5f;

        StartCoroutine(candle(Random.Range(LimIntensity.x, LimIntensity.y)));
    }

    IEnumerator candle(float targetIntencity)
    {

        if (up)
        {
            while(lgt.intensity < targetIntencity)
            {
                lgt.intensity += Time.deltaTime * SpeedChange;
                yield return new WaitForSeconds(Random.Range(.1f, .2f));
            }
            up = false;
            StartCoroutine(candle(Random.Range(LimIntensity.x, LimIntensity.y)));
        }
        else
        {
            while (lgt.intensity > targetIntencity)
            {
                lgt.intensity -= Time.deltaTime * SpeedChange;
                yield return new WaitForSeconds(Random.Range(.1f, .2f));
            }
            up = true;
            StartCoroutine(candle(Random.Range(LimIntensity.x, LimIntensity.y)));
        }
    }
}
