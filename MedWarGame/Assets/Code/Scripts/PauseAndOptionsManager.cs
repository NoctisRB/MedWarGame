using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseAndOptionsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseCanvas = default;
    [SerializeField] private GameObject troopSelector = default;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public void PauseUnpause()
    {
        if (!gameIsPaused)
        {
            //enseñar el pause canvas
            pauseCanvas.SetActive(true);
            //esconder el troop selector
            troopSelector.SetActive(false);
            gameIsPaused = true;
            Time.timeScale = 0f;
        }
        else
        {
            //esconder el pause canvas
            pauseCanvas.SetActive(false);
            //devolver el troop selector
            troopSelector.SetActive(true);
            gameIsPaused = false;
            Time.timeScale = 1f;
        }
    }
}
