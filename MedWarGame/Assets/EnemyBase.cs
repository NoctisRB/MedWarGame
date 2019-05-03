using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{

    [SerializeField]
    private float _minTimeSpawn;

    [SerializeField]
    private float _maxTimeSpawn;

    [SerializeField]
    private float _energyPerSecond;

    private float _timeSpawn;

    [SerializeField]
    private float _currentEnergy;

  

    // Start is called before the first frame update
    void Start()
    {
        _timeSpawn = Random.Range(_minTimeSpawn, _maxTimeSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= _timeSpawn)
        {
            Invoke("ConsiderSpawn", 0);
            _timeSpawn = Time.time + Random.Range(_minTimeSpawn, _maxTimeSpawn);
        }

        _currentEnergy += _energyPerSecond * Time.deltaTime;

    }

    private void ConsiderSpawn()
    {
        if (_currentEnergy < 10)
        {
            return;
        }
        else if (_currentEnergy < 25)
        {
            //Spawn Dwarf or Elf
            Debug.Log("DWARF or ELF");
            SpawnTroop();
            return;
        }
        else
        {
            //Spawn Orc or Wizard
            Debug.Log("ORC or WIZARD or DWARF or ELF");
            SpawnTroop();
            return;
        }
    }

    private void SpawnTroop()
    {
        //Spawn troop that must be send by COnsider Spawn
        //Rest Cost of the spawned troop to _currentEnergy
        Debug.Log("SPAWN");
        return;
    }
}
