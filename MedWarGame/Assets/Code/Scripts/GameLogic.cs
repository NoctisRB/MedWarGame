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

    private Text timerText;
    [SerializeField]
    private float timer;
    

    void Start()
    {
        _endPanel.SetActive(false);
        timer = Time.time;
    }

    void Update()
    {
        float t = Time.time - timer;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString();

        timerText.text = "Your time was " + minutes + ":" + seconds;

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");

        /*if(_enemies == null && _enemyBases == null)
        {
            _endPanel.SetActive(true);
        }*/

        if (Input.GetKey(KeyCode.Space))
        {
            _endPanel.SetActive(true);
        }
    }
}
