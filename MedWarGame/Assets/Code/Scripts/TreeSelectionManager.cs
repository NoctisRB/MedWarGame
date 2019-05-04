using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSelectionManager : MonoBehaviour
{
    // Start is called before the first frame update
    treeScript[] trees = default;
    [SerializeField] private GameObject treesParent = default;
    private treeScript selectedTree;

    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Player")
                {
                    selectedTree = hit.collider.GetComponent<treeScript>();
                    foreach (treeScript t in trees)
                    {
                       t.isSelected = false;
                    }
                    selectedTree.isSelected = true;
                }
            }
        }
    }
}
