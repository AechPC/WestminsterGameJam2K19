using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup panelGroup;

    private void Start()
    {
        ContinueGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Remember to place the level as the next build index.
    }

    public void ControlsScreen()
    {
        SceneManager.LoadScene(2); //Assuming scene 2 is the controls screen.
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMain()
    {
        SceneManager.LoadScene(0); //Assuming that main menu is build index 0, which it always should be.
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        panelGroup.alpha = 1;
        panelGroup.interactable = true;
        panelGroup.blocksRaycasts = true;
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        panelGroup.alpha = 0;
        panelGroup.interactable = false;
        panelGroup.blocksRaycasts = false;
    }
}