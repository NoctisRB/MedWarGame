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

    private bool _ended = false;

    void Start()
    {
        _endPanel.SetActive(false);
       

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!_ended)
            {
                string minutes = ((int)timer / 60).ToString();
                string seconds = Mathf.Round((timer % 60)).ToString();

                timerText.text = "Your time was\n " + minutes + "' " + seconds + "''";
                _endPanel.SetActive(true);
                _ended = true;
            }
          
        }
        else
        {
            timer += Time.deltaTime;
        }
        

        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _enemyBases = GameObject.FindGameObjectsWithTag("EnemyBase");

        /*if(_enemies == null && _enemyBases == null)
        {
            _endPanel.SetActive(true);
        }*/

       
    }
}
