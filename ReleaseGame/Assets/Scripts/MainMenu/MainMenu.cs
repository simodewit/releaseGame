using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region variables

    [Header("All the refrences to the panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject playPanel;
    public GameObject quitGamePanel;

    [Header("refrences to use")]
    public AudioSource buttonClickSound;
    public int sceneLoadIndex;

    #endregion

    #region button functions

    public void OnClickPlay()
    {
        //buttonClickSound.Play();
        mainPanel.SetActive(false);
        playPanel.SetActive(true);
    }

    public void OnClickJoinGame()
    {
        //buttonClickSound.Play();
        SceneManager.LoadScene(sceneLoadIndex);
    }

    public void OnClickSettings()
    {
        //buttonClickSound.Play();
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnClickQuitGame()
    {
        //buttonClickSound.Play();
        mainPanel.SetActive(false);
        quitGamePanel.SetActive(true);
    }

    public void OnClickQuitGameNo()
    {
        //buttonClickSound.Play();
        quitGamePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnClickQuitGameYes()
    {
        //buttonClickSound.Play();
        Application.Quit();
    }

    public void OnClickBackFromPlay()
    {
        //buttonClickSound.Play();
        playPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void OnClickBackFromSettings()
    {
        //buttonClickSound.Play();
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    #endregion
}
