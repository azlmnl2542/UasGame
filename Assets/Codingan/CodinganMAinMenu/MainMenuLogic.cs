using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject PanelMainMenu;
    public GameObject PanelOption;

    void Start()
    {
        PanelMainMenu.SetActive(true);
        PanelOption.SetActive(false);
    }

    public void OpenOption()
    {
        PanelMainMenu.SetActive(false);
        PanelOption.SetActive(true);
    }

    public void CloseOption()
    {
        PanelMainMenu.SetActive(true);
        PanelOption.SetActive(false);
    }

    public void OpenGamePlay()
    {
        SceneManager.LoadScene("MazeStage");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    void Update()
    {

    }
}