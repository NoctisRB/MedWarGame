using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopSpawnerManager : MonoBehaviour
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

    [SerializeField] private Button dwarfButton = default;
    [SerializeField] private Button elveButton = default;
    [SerializeField] private Button ogreButton = default;
    [SerializeField] private Button wizardButton = default;

    // Start is called before the first frame update
    void Start()
    {
        selectedTroop = Troop.none;
        dwarfButton.onClick.AddListener(InstantiateDwarf);
        elveButton.onClick.AddListener(InstantiateElve);
        ogreButton.onClick.AddListener(InstantiateOgre);
        wizardButton.onClick.AddListener(InstantiateWizard);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
