using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float energyPerSecond = 1f;
    //private float currentEnergy;
    public bool isPlayer = true;
    public bool isSelected;   
    public float GetEnergy()
    {
        return energyPerSecond;
    }
}
