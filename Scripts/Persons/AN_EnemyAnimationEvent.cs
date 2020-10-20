using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AN_EnemyAnimationEvent : MonoBehaviour
{
    public AN_EnemyBaheviour baheviour;

    public void Event()
    {
        baheviour.AnimAtack();
    }
}
