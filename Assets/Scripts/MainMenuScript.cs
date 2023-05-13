using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSim()
    {
        SceneManager.LoadScene("Main VR Scene", LoadSceneMode.Single);
    }

    public void QuitSim()
    {
        Application.Quit();
    }
}
