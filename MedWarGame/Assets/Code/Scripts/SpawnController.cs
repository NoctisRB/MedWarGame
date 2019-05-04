using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject treesParent =  default;
    treeScript[] trees = default;
    [SerializeField] private TroopSelectionManager troopSelection = default;
    // Start is called before the first frame update
    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
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

        Instantiate(troopPrefab, GenerateRandomPosition(treePos, deployRange), Quaternion.identity);
    }

    private Vector3 GenerateRandomPosition(Vector3 treePos, float deployRange)
    {
        float xPos = Random.Range(treePos.x - deployRange, treePos.x + deployRange);
        float zPos = Random.Range(treePos.z - deployRange, treePos.z + deployRange);
        return new Vector3(xPos, 0, zPos);
    }
}
