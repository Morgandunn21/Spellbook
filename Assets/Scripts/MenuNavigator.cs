using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuNavigator : MonoBehaviour
{
    [Header("Game Screens")]
    public GameObject helpScreen;
    public GameObject pauseScreen;
    public GameObject endScreenInstance;
    public GameObject spellBook;
    public GameObject interactPromptInstance;
    [Header("Level Fade")]
    public Image fade;
    public float fadeTime;

    public static GameObject endScreen;
    public static GameObject interactPrompt;

    public void Awake()
    {
        interactPrompt = interactPromptInstance;
        endScreen = endScreenInstance;

        StartCoroutine(Fade(true));
    }

    public static void showInteractPrompt(string promptText)
    {
        interactPrompt.GetComponent<TextMeshProUGUI>().text = promptText;
        interactPrompt.SetActive(true);
    }

    public static void hideInteractPrompt()
    {
        interactPrompt.SetActive(false);
    }

    public void openTutorial()
    {
        if(helpScreen != null)
        {
            helpScreen.SetActive(true);
        }
    }

    public void exitTutorial()
    {
        if (helpScreen != null)
        {
            helpScreen.SetActive(false);
        }
    }

    public void openPauseMenu()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void exitPauseMenu()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void startGame()
    {
        StartCoroutine(Fade(false));

        Invoke("EndStartScreen", fadeTime);
    }

    public void exitGame()
    {
        Application.Quit(0);
    }

    public void goToStartScreen()
    {
        SceneManager.LoadScene("Start Screen");
    }

    public void openSpellbook()
    {
        Time.timeScale = 0;
        spellBook.SetActive(true);
    }

    public void closeSpellbook()
    {
        Time.timeScale = 1;
        spellBook.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen != null)
            {
                if (pauseScreen.activeSelf)
                {
                    exitPauseMenu();
                }
                else
                {
                    openPauseMenu();
                }
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if (spellBook != null)
            {
                if (spellBook.activeSelf)
                {
                    closeSpellbook();
                }
                else
                {
                    openSpellbook();
                }
            }
        }
    }

    public static void showEndScreen()
    {
        Time.timeScale = 0;
        endScreen.SetActive(true);
    }

    public void endLevel(int levelNum)
    {
        StartCoroutine(Fade(false));

        Invoke("endLevelCoroutine", fadeTime);
    }

    public void endLevelCoroutine(int levelNum)
    {
        switch (levelNum)
        {
            case 0:
                endHubLevel();
                break;
            case 1:
                endLevelOne();
                break;
            case 2:
                endLevelTwo();
                break;
        }
    }

    public void EndStartScreen()
    {
        SceneManager.LoadScene("Hub World");
    }

    public void endHubLevel()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void endLevelOne()
    {
        SceneManager.LoadScene("Level 2");
    }

    public void endLevelTwo()
    {
        showEndScreen();
    }

    private IEnumerator Fade(bool fadeIn)
    {
        fade.gameObject.SetActive(true);

        float time = 0;

        while (time < fadeTime)
        {
            float fadeRate = Time.deltaTime / fadeTime;

            if (fadeIn)
            {
                fadeRate *= -1;
            }

            Color temp = fade.color;
            temp.a += fadeRate;
            fade.color = temp;

            yield return null;

            Debug.Log($"Alpha: {temp.a}");

            time += Time.deltaTime;
        }

        if(fadeIn)
        {
            Color temp = fade.color;
            temp.a = 0;
            fade.color = temp;

            fade.gameObject.SetActive(false);
        }
        else
        {
            Color temp = fade.color;
            temp.a = 1;
            fade.color = temp;
        }
    }
}
