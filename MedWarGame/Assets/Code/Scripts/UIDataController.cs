using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDataController : MonoBehaviour
{
    [SerializeField] private Text currentEnergyText = default;
    [SerializeField] private Text energyPerSecText = default;
    [SerializeField] private CurrencyManager currencyManager = default;

    // Update is called once per frame
    void Update()
    {
        currentEnergyText.text = "Energy: " + Convert.ToString(Mathf.FloorToInt(currencyManager.currentCurrency));
        energyPerSecText.text = "Energy/sec: " + Convert.ToString(currencyManager.GetCurrentCurrencyPerSec());
    }
}
