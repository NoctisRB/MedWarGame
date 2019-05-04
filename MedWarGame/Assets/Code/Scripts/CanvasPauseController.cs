using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasPauseController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void Resume()
    {
        GameObject.Find("GameManager").GetComponent<PauseAndOptionsManager>().PauseUnpause();
    }
    public void MainMenu()
    {
        //Load MainMenu Scene
    }
}
