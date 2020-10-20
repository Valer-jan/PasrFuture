using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_ColorGradingZone : MonoBehaviour
{
    public int Temperature = 0;
    private void OnTriggerEnter(Collider other)
    {
        AN_ColorGrading grad = other.GetComponent<AN_ColorGrading>();
        if (grad != null) grad.setTemp = Temperature;
    }
}
