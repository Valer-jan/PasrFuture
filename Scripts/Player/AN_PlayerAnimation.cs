using UnityEngine;

public class AN_PlayerAnimation : MonoBehaviour
{
    public Animator Animator; // AnimatorController

    AN_GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<AN_GameManager>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Animator.SetBool("isStoped", false);

            if (manager.StartGamePlayTrigger) Animator.SetBool("with", true);
            else Animator.SetBool("with", false);
        }
        else
        {
            Animator.SetBool("isStoped", true);
        }
    }
}
