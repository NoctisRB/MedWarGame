using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [HideInInspector] public float currentCurrency = 0;
    [SerializeField] private GameObject treesParent;
    private treeScript[] trees = null;
    // Start is called before the first frame update
    void Start()
    {
        trees = treesParent.GetComponentsInChildren<treeScript>();
        currentCurrency = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentCurrency += GetCurrentCurrencyPerSec() * Time.deltaTime;
    }

    public float GetCurrentCurrencyPerSec()
    {
        float energy = 0;
        foreach(treeScript t in trees)
        {
            if (t.isPlayer) energy += t.GetEnergy();
        }
        return energy;
    }
}
