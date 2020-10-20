using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class AN_PauseMenu : MonoBehaviour
{
    public PostProcessVolume PostProc;

    bool Paused = false;
    Canvas canvas;
    DepthOfField Depth = null;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        PostProc.profile.TryGetSettings(out Depth);
    }


    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) Paused = !Paused;

        if (Paused)
        {
            Depth.focusDistance.value = 1f;

            Time.timeScale = 0f;
            canvas.enabled = true;
        }
        else
        {
            if (Depth.focusDistance.value < 10f) Depth.focusDistance.value += Time.unscaledDeltaTime * 10;

            Time.timeScale = 1f;
            canvas.enabled = false;
        }
    }

    // button event
    public void CLickPlay()
    {
        Paused = false;
    }

    public void CLickQuit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
