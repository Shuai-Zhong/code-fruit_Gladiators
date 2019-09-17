using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private bool paused;
    private GameManager GM;

    public GameObject resumeButton;
    public GameObject pauseOverlay;
    public GameObject eventSystem;

    private void Awake()
    {
        GM = FindObjectOfType<GameManager>();
    }

    private void Start ()
    {
        paused = false;

        pauseOverlay.SetActive(false);
	}
	
	private void Update ()
    {
        SetTimeScale();
    }

    // Switch timescale when flipping pause
    private void SetTimeScale()
    {
        if (!paused)
        {
            Time.timeScale = 1;
        }
        else if (paused)
        {
            Time.timeScale = 0;
        }
    }

    // Quit current stage
    public void QuitStage()
    {
        paused = !paused;
        GM.LoadMainMenu();
    }

    // Handle pause press
    public void PressPause()
    {
        paused = !paused;
        pauseOverlay.SetActive(!pauseOverlay.activeInHierarchy);

        if (pauseOverlay.activeInHierarchy)
        {
            eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(resumeButton);
        }
    }

    // Get pause state
    public bool GetPausedState()
    {
        return paused;
    }
}
