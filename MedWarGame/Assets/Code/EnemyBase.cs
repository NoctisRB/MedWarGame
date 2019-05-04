using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBase : MonoBehaviour
{
    private enum Troop
    {
        dwarf,
        elve,
        ogre,
        wizard
    }
    private Troop spawnedTroop;

    [SerializeField] private GameObject dwarfPrefab = default;
    [SerializeField] private GameObject elvePrefab = default;
    [SerializeField] private GameObject ogrePrefab = default;
    [SerializeField] private GameObject wizardPrefab = default;

    private GameObject spawnablePrefab;

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

            int r = Random.Range(0, 1);
            if (r == 0) spawnedTroop = Troop.dwarf;
            else spawnedTroop = Troop.elve;
            SpawnTroop();
            return;
        }
        else
        {
            //Spawn Orc or Wizard
            //Debug.Log("ORC or WIZARD or DWARF or ELF");
            int r = Random.Range(0, 3);
            if (r == 0) spawnedTroop = Troop.dwarf;
            else if (r == 1) spawnedTroop = Troop.elve;
            else if(r == 2) spawnedTroop = Troop.ogre;
            else spawnedTroop = Troop.wizard;
            SpawnTroop();
            return;
        }
    }

    private void SpawnTroop()
    {
        //Spawn troop that must be send by COnsider Spawn
        Instantiate(GetSpawneableTroop(), GenerateRandomPosition(GetSpawneableTroop().GetComponent<enemyTroopScript>().GetDeployRange()), Quaternion.identity);
        //Rest Cost of the spawned troop to _currentEnergy
        _currentEnergy -= GetSpawneableTroop().GetComponent<enemyTroopScript>().GetCost();
        //Debug.Log("SPAWN");
        return;
    }
    
    private GameObject GetSpawneableTroop()
    {
        if (spawnedTroop == Troop.dwarf) return dwarfPrefab;
        else if (spawnedTroop == Troop.elve) return elvePrefab;
        else if (spawnedTroop == Troop.ogre) return ogrePrefab;
        else return wizardPrefab;
    }

    private Vector3 GenerateRandomPosition(float deployRange)
    {
        float xPos = Random.Range(this.gameObject.transform.position.x - deployRange, this.gameObject.transform.position.x + deployRange);
        float zPos = Random.Range(this.gameObject.transform.position.z - deployRange, this.gameObject.transform.position.z + deployRange);
        return new Vector3(xPos, 0, zPos);
    }
}
