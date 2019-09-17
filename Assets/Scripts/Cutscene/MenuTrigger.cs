using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTrigger : MonoBehaviour
{
    // Game Over Buttons; 0: Retry button 1: Menu Button
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private GameObject menuButtons;
    [SerializeField] private EndScoreHandler scoreHandler;

    private void Start()
    {
        menuButtons.SetActive(false);
    }

    // Make the menu visible and allows button presses
    public void EnableMenu()
    {
        menuButtons.SetActive(true);
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[0]);
    }

    // Reload last scene if retry is pressed
    public void Retry()
    {
        SceneManager.LoadScene(scoreHandler.lastScene);
    }

    // Go back to menu if menu is pressed
    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
}
