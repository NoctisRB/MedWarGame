using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject treesParent;
    private treeScript[] trees;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject defeatPanel;
    private bool allDestroyed;

    [SerializeField]
    private GameObject[] _bars;


    [SerializeField]
    private Text timerText;
    private float timer = 0;

    // Update is called once per frame
    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
       
        Time.timeScale = 1;
    }

    private void Update()
    {

        timer += Time.deltaTime;

        allDestroyed = false;
        foreach (treeScript t in trees)
        {
            if (t.isPlayer && t.GetHp() <= 0) Lose();
            if (!t.isPlayer && t.GetHp() > 0)
            {
                allDestroyed = false;
                return;
            }
            if (!t.isPlayer && t.GetHp() <= 0)
            {
                allDestroyed = true;
            }
        }
        if (allDestroyed) Win();   
    }

    private void Win()
    {
        foreach (var bar in _bars)
        {
            bar.SetActive(false);
        }
        Time.timeScale = 0;

        string minutes = ((int)timer / 60).ToString();
        string seconds = Mathf.Round((timer % 60)).ToString();

        timerText.text = "Your time was\n " + minutes + "' " + seconds + "''";
        winPanel.SetActive(true);
    }

    private void Lose()
    {
        Time.timeScale = 0;
        defeatPanel.SetActive(true);
    }
}
