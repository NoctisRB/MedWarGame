using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    private GameObject treesParent;
    private treeScript[] trees;
    [SerializeField] private TroopSelectorManager troopSelection = default;
    // Start is called before the first frame update
    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private treeScript GetActiveTree()
    {
        foreach(treeScript t in trees)
        {
            if (t.isSelected) return t;
        }
        return null;
    }

    private void SpawnTroop()
    {
        Vector3 treePos = GetActiveTree().gameObject.transform.position;
        float deployRange = troopSelection.GetSelectedTroop().GetDeployRange();
        GameObject troopPrefab = troopSelection.GetSelectedTroopPrefab();


    }

    private void GenerateRandomPosition(Vector3 treePos, float deployRange)
    {

    }
}
