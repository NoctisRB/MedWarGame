using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCanvasScript : MonoBehaviour
{
    public static bool playerIsInSettings = false;
    [SerializeField] GameObject SettingsCanvas;
    [SerializeField] GameObject ExitButton;    
    [SerializeField] GameObject SettingsButton;

    private void Update()
    {
        if(playerIsInSettings && Input.GetKeyDown(KeyCode.Escape))
        {
            SettingsButton.SetActive(true);
            ExitButton.SetActive(true);           
            SettingsCanvas.SetActive(false);
            ChangePlayerIsInSettings();
        }
    }
    public void ChangePlayerIsInSettings()
    {
        if (playerIsInSettings) playerIsInSettings = false;
        else playerIsInSettings = true;
    }

    

    public void ExitGame()
    {
        Application.Quit();
    }
}
