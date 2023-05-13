using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public Slider spoolSlider, diamSlider;
    public Button pauseButton, resumeButton, adjustButton;
    public GameObject promptMenu;
    // Start is called before the first frame update
    void Start()
    {
        resumeButton.interactable = false;
        promptMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
        spoolSlider.interactable = false;
        diamSlider.interactable = false;
        pauseButton.interactable = false;
        adjustButton.interactable = false;
        resumeButton.interactable = true;
        promptMenu.SetActive(true);
    }

    public void ResumeButton()
    {
        Time.timeScale = 1f;
        spoolSlider.interactable = true;
        diamSlider.interactable = true;
        resumeButton.interactable = false;
        pauseButton.interactable = true;
        adjustButton.interactable = true;
        promptMenu.SetActive(false);
    }

    public void ExitToMenu()
    {
        if(Time.timeScale != 1f)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
