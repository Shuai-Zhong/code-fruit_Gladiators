using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    // Holds different menus
    // 0: Main, 1: Play 2:Credits, 3: Main Menu Buttons, 4: HowtoPlay
    [SerializeField] private GameObject[] menus;

    // Holds buttons for menu navigation
    // 0: Play, 1: StageOne, 2: CreditsBack, 4: HowToPlayBack
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private GameObject[] charPrefabs;
    [SerializeField] private Animator menuAnim;
    [SerializeField] private GameObject eventSystem;
    [SerializeField] private TextMeshProUGUI difficultyText;

    private int difficulty;

    void Start ()
    {
        SetMenusStart();
        InvokeRepeating("SpawnChar", 5f, 15f);

        GetDifficultyPref();
    }

    //Set menus on start
    private void SetMenusStart()
    {
        menus[0].SetActive(true);
        menus[1].SetActive(false);
        menus[2].SetActive(false);
        menus[4].SetActive(false);
    }

    // Open HowToPlay
    public void OpenHowToPlay()
    {
        menuAnim.SetTrigger("ToHowToPlay");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[3]);
    }

    // Return from HowToPlay to main menu
    public void HowToPlayToMain()
    {
        menuAnim.SetTrigger("HowToPlayToMain");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[0]);
    }

    // Open Credits
    public void OpenCredits()
    {
        menuAnim.SetTrigger("ToCredits");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[2]);
    }

    // Return from Credits to main menu
    public void CreditsToMain()
    {
        menuAnim.SetTrigger("CreditsToMain");
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[0]);
    }

    // Open Level Select
    public void OpenPlayMenu()
    {
        menus[3].SetActive(false);
        menus[1].SetActive(true);
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[1]);
    }

    // From Level Screen to Main
    public void PlaytoMain()
    {
        menus[3].SetActive(true);
        menus[1].SetActive(false);
        eventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(buttons[0]);
    }

    // Start Stage 1
    public void PlayStageOne()
    {
        SceneManager.LoadScene(2);
    }

    // Start Stage 2
    public void PlayStageTwo()
    {
        SceneManager.LoadScene(3);
    }

    // Start Stage 3
    public void PlayStageThree()
    {
        SceneManager.LoadScene(4);
    }

    // Exit game
    public void ExitGame()
    {
        Application.Quit();
    }

    // Spawn background character
    private void SpawnChar()
    {
        // Rolls random with 66% chance of spawning
        int charRoll = Random.Range(0, 3);
        if(charRoll < 2)
        {
            Instantiate(charPrefabs[charRoll]);
        }
    }

    // Set Difficulty via preference
    private void GetDifficultyPref()
    {
        if (!PlayerPrefs.HasKey("Difficulty"))
        {
            PlayerPrefs.SetInt("Difficulty", 0);
            difficulty = 0;
        }
        else
        {
            difficulty = PlayerPrefs.GetInt("Difficulty");
        }

        SetDifficultyText();
    }

    // Change difficulty
    public void ChangeDifficulty()
    {
        if(difficulty < 2)
        {
            difficulty++;
        }
        else
        {
            difficulty = 0;
        }

        PlayerPrefs.SetInt("Difficulty", difficulty);

        SetDifficultyText();
    }

    // Set text for difficulty
    private void SetDifficultyText()
    {
        switch (difficulty)
        {
            case 0:
                difficultyText.text = "Easy";
                break;

            case 1:
                difficultyText.text = "Normal";
                break;

            case 2:
                difficultyText.text = "Hard";
                break;
        }
    }
}
