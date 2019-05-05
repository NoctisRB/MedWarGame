using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject treesParent;
    private treeScript[] trees;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject defeatPanel;
    private bool allDestroyed;
    // Update is called once per frame
    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
    }

    private void Update()
    {
        allDestroyed = false;
        foreach(treeScript t in trees)
        {
            if (t.isPlayer && t.GetHp() <= 0) Lose();
            if (!t.isPlayer && t.GetHp() > 0)
            {
                allDestroyed = false;
                return;
            }
            if (!t.isPlayer && t.GetHp() <= 0) allDestroyed = true;
        }
        if (allDestroyed) Win();
    }

    private void Win()
    {
        Time.timeScale = 0;
        winPanel.SetActive(true);
    }
    private void Lose()
    {
        Time.timeScale = 0;
        defeatPanel.SetActive(true);
    }
}
