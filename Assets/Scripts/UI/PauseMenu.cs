using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    protected TimeScaleEditor timeScaleEditor;
    public GameObject pauseMenuUI;

    // Start is called before the first frame update
    void Start()
    {
        timeScaleEditor = GameObject.FindObjectOfType<TimeScaleEditor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        timeScaleEditor.ReleaseUnsetTimeScale();
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        timeScaleEditor.ForceSetTimeScale(0f);
        GameIsPaused = true;
    }

    public void ReloadLevel()
    {
        timeScaleEditor.ForceSetTimeScale(1f);
        GameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadHomeMenu()
    {
        timeScaleEditor.ReleaseUnsetTimeScale();
        GameIsPaused = false;
        SceneManager.LoadScene("HomeMenu");
    }
}
