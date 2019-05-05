using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    private GameObject[] _enemies;
    private GameObject[] _enemyBases;

    [SerializeField]
    private GameObject _endPanel;

    [SerializeField]
    private Text timerText;

    private float timer = 0;

    void Start()
    {
        _endPanel.SetActive(false);
        Time.timeScale = 1;

    }

    void Update()
    {

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");
        if (Input.GetKey(KeyCode.Space))
        {
            if (_enemies == null && _enemyBases == null)
            {
                string minutes = ((int)timer / 60).ToString();
                string seconds = Mathf.Round((timer % 60)).ToString();

                timerText.text = "Your time was\n " + minutes + "' " + seconds + "''";
                _endPanel.SetActive(true);
            }          
        }
        else if (_enemies == null && _enemyBases == null)
        {
            string minutes = ((int)timer / 60).ToString();
            string seconds = Mathf.Round((timer % 60)).ToString();

            timerText.text = "Your time was\n " + minutes + "' " + seconds + "''";
            _endPanel.SetActive(true);
        }
        else
        {
            timer += Time.deltaTime;
        }
        
  
    }
}
