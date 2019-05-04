using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CanvasPauseController : MonoBehaviour
{
    public AudioSource pauseSound;
    public GameObject[] buttons;

    // Start is called before the first frame update
    void Start()
    {
        pauseSound.Play();
    }

    void Update()
    {
        for(int i = 0; i <= buttons.Length - 1; i++)
        {
            if (buttons[i].)
        }
    }
    
    public void Resume()
    {
        GameObject.Find("GameManager").GetComponent<PauseAndOptionsManager>().PauseUnpause();
    }
    public void MainMenu()
    {
        //Load MainMenu Scene
    }
}
