using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuManager : MonoBehaviour
{
    public GameObject OptionsMenu;

    public GameObject ResumeButton;
    public GameObject OptionsButton;
    public GameObject MainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        OptionsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OptionsMenuActivator()
    {
        OptionsMenu.SetActive(true);

        ResumeButton.SetActive(false);
        OptionsButton.SetActive(false);
        MainMenuButton.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
