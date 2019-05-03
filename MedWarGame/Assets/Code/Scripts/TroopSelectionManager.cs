using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopSelectorManager : MonoBehaviour
{
    public enum Troop
    {
        none,
        dwarf,
        elve,
        ogre,
        wizard
    }
    public Troop selectedTroop;

    [SerializeField] private GameObject dwarfPrefab = default;
    [SerializeField] private GameObject elvePrefab = default;
    [SerializeField] private GameObject ogrePrefab = default;
    [SerializeField] private GameObject wizardPrefab = default;

    private troopScript dwarf;
    private troopScript elve;
    private troopScript ogre;
    private troopScript wizard;

    [SerializeField] private Button dwarfButton = default;
    [SerializeField] private Button elveButton = default;
    [SerializeField] private Button ogreButton = default;
    [SerializeField] private Button wizardButton = default;

    void Start()
    {
        dwarf = dwarfPrefab.GetComponent<troopScript>();
        elve = elvePrefab.GetComponent<troopScript>();
        ogre = ogrePrefab.GetComponent<troopScript>();
        wizard = wizardPrefab.GetComponent<troopScript>();

        selectedTroop = Troop.none;
        dwarfButton.onClick.AddListener(InstantiateDwarf);
        elveButton.onClick.AddListener(InstantiateElve);
        ogreButton.onClick.AddListener(InstantiateOgre);
        wizardButton.onClick.AddListener(InstantiateWizard);
    }

    private void InstantiateDwarf()
    {
        selectedTroop = Troop.dwarf;
        Debug.Log("Dward Selected");
    }
    private void InstantiateElve()
    {
        selectedTroop = Troop.elve;
        Debug.Log("Elve Selected");
    }
    private void InstantiateOgre()
    {
        selectedTroop = Troop.ogre;
        Debug.Log("Ogre Selected");
    }
    private void InstantiateWizard()
    {
        selectedTroop = Troop.wizard;
        Debug.Log("Wizard Selected");
    }

    public troopScript GetTroopSelected()
    {
        if (selectedTroop == Troop.dwarf) return dwarf;
        else if (selectedTroop == Troop.elve) return elve;
        else if (selectedTroop == Troop.ogre) return ogre;
        else if (selectedTroop == Troop.wizard) return wizard;
        else return null;
    }
}
