using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILogic : MonoBehaviour
{
    public GameObject PanelMainmenu;
    public GameObject PanelAbout;
    

    public void NavigasiAbout()
    {
        PanelMainmenu.SetActive(false);
        PanelAbout.SetActive(true);
    }

    public void NavigasiMainmenu()
    {
        PanelMainmenu.SetActive(true);
        PanelAbout.SetActive(false);
    }
    
    public void NavigasiGameplay()
    {
        SceneManager.LoadScene("MazeStage");
    }
}
