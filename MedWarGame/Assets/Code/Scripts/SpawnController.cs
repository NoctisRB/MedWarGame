using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject treesParent =  default;
    private treeScript[] trees;
    [SerializeField] private TroopSelectionManager troopSelection = default;
    private CurrencyManager currencyManager;
    // Start is called before the first frame update
    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
        currencyManager = GameObject.Find("GameManager").GetComponent<CurrencyManager>();
    }

    private treeScript GetActiveTree()
    {
        foreach(treeScript t in trees)
        {
            if (t.isSelected) return t;
        }
        Debug.Log("No tree selected");
        return null;
    }

    public void SpawnTroop()
    {
        Vector3 treePos = GetActiveTree().gameObject.transform.position;
        float deployRange = troopSelection.GetSelectedTroop().GetDeployRange();
        GameObject troopPrefab = troopSelection.GetSelectedTroopPrefab();
        if (!PauseAndOptionsManager.gameIsPaused && currencyManager.currentCurrency >= troopSelection.GetSelectedTroopPrefab().GetComponent<troopScript>().GetCost())
        {
            Instantiate(troopPrefab, GenerateRandomPosition(treePos, deployRange), Quaternion.identity);
            currencyManager.currentCurrency -= troopSelection.GetSelectedTroopPrefab().GetComponent<troopScript>().GetCost();
        }
            
    }

    private Vector3 GenerateRandomPosition(Vector3 treePos, float deployRange)
    {
        float xPos = Random.Range(treePos.x - deployRange, treePos.x + deployRange);
        float zPos = Random.Range(treePos.z - deployRange, treePos.z + deployRange);
        return new Vector3(xPos, 0, zPos);
    }

    public treeScript[] GetTrees()
    {
        return trees;
    }
}
