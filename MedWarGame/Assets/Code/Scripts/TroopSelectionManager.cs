using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopSelectionManager : MonoBehaviour
{
    public enum Troop
    {
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

    private SpawnController spawnController;

    void Start()
    {
        dwarf = dwarfPrefab.GetComponent<troopScript>();
        elve = elvePrefab.GetComponent<troopScript>();
        ogre = ogrePrefab.GetComponent<troopScript>();
        wizard = wizardPrefab.GetComponent<troopScript>();

        dwarfButton.onClick.AddListener(InstantiateDwarf);
        elveButton.onClick.AddListener(InstantiateElve);
        ogreButton.onClick.AddListener(InstantiateOgre);
        wizardButton.onClick.AddListener(InstantiateWizard);

        spawnController = GameObject.Find("GameManager").GetComponent<SpawnController>();
    }

    private void InstantiateDwarf()
    {
        selectedTroop = Troop.dwarf;
        spawnController.SpawnTroop();
    }
    private void InstantiateElve()
    {
        selectedTroop = Troop.elve;
        spawnController.SpawnTroop();
    }
    private void InstantiateOgre()
    {
        selectedTroop = Troop.ogre;
        spawnController.SpawnTroop();
    }
    private void InstantiateWizard()
    {
        selectedTroop = Troop.wizard;
        spawnController.SpawnTroop();
    }

    public troopScript GetSelectedTroop()
    {
        if (selectedTroop == Troop.dwarf) return dwarf;
        else if (selectedTroop == Troop.elve) return elve;
        else if (selectedTroop == Troop.ogre) return ogre;
        else return wizard;
    }

    public GameObject GetSelectedTroopPrefab()
    {
        if (selectedTroop == Troop.dwarf) return dwarfPrefab;
        else if (selectedTroop == Troop.elve) return elvePrefab;
        else if (selectedTroop == Troop.ogre) return ogrePrefab;
        else return wizardPrefab;
    }

    private IEnumerator AnimateButton(GameObject button, bool up)
    {
        if (up)
        {
            float yPos = Mathf.Lerp(button.GetComponent<RectTransform>().anchoredPosition.y, -121, 0.1f);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x, yPos);
            if(button.GetComponent<RectTransform>().anchoredPosition.y <= -121.5) yield return null;
        }
        else
        {
            float yPos = Mathf.Lerp(button.GetComponent<RectTransform>().anchoredPosition.y, -185, 0.1f);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x, yPos);
            if (button.GetComponent<RectTransform>().anchoredPosition.y >= -174.5) yield return null;
        }
    }
}
