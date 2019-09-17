using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    private string[] oneLiners = new string[]
    {
        "Yikes!",
        "Thank you, come again!",
        "They don't think it be like it is, but it do.",
        "Magnets... how do they work?",
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce consequat... WHOOPS! Why is this still here?",
        "Sonic '06 is a good game.",
        "SOMEBODY THOUCHA MY SPAGHET !!!!!",
        "Whazaaa",
        "Most Heinous!",
        "Be excellent to each other!",
        "YOU MUST DEFEAT SHENG LONG TO STAND A CHANCE.",
        "Omae wa mou shindeiru..."
    };

    [SerializeField] private Animator playerAnim;
    private TextMeshProUGUI oneLiner;
    private float counter;
    private bool playerAnimCalled;

    public int buildSpeed;

    private void Awake()
    {
        oneLiner = GetComponent<TextMeshProUGUI>();
    }

    private void Start ()
    {
        playerAnimCalled = false;

        DisplayOneLiner();
	}

    private void Update()
    {
        UpdateOneLinerChars();
    }

    // Pick random one liner and display it
    private void DisplayOneLiner()
    {
        oneLiner.maxVisibleCharacters = 0;

        int rndText = Random.Range(0, oneLiners.Length);
        oneLiner.text = oneLiners[rndText];
    }

    // Build up one liner letter by letter if not already complete
    private void UpdateOneLinerChars()
    {
        if (oneLiner.maxVisibleCharacters < oneLiner.textInfo.characterCount)
        {
            counter += (buildSpeed * Time.deltaTime) * oneLiner.textInfo.characterCount;
        }
        else
        {
            if (!playerAnimCalled)
            {
                playerAnimCalled = true;
                playerAnim.SetTrigger("TextFinished");
            }
        }

        oneLiner.maxVisibleCharacters = (int)counter;
    }
}
