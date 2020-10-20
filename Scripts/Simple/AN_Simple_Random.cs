using UnityEngine;

public class AN_Simple_Random : MonoBehaviour
{
    [Header("Rotation")]
    public bool RandomStartRotate = true;
    public Vector3 MaxRotate;
    [Space]
    public bool ConstRotate = false;
    public Vector3 SpeedRotate;

    [Header("Positioning")]
    public bool SinLifting = false;
    public Vector3 Magnitude;
    public float LiftSpeed = 1f;
    Vector3 startPos;

    private void Start()
    {
        if (RandomStartRotate) transform.Rotate(Random.Range(0, MaxRotate.x), Random.Range(0, MaxRotate.y), Random.Range(0, MaxRotate.z));

        startPos = transform.position;
    }

    private void Update()
    {
        if (ConstRotate) transform.Rotate(SpeedRotate * Time.deltaTime);

        if (SinLifting) transform.position = startPos + Magnitude * Mathf.Sin(Time.time * LiftSpeed);
    }
}
