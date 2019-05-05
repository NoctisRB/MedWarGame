using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class TroopSelectionManager : MonoBehaviour
{
    public Camera cam;
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

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    private GameObject buttonsParent;
    private buttonAnimation[] buttons;

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

        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();

        buttonsParent = this.gameObject;
        buttons = buttonsParent.GetComponentsInChildren<buttonAnimation>();
    }

    private void Update()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        foreach (buttonAnimation b in buttons)
        {
            if (!b.gameObject.GetComponent<buttonAnimation>().isMoving && !b.gameObject.GetComponent<buttonAnimation>().up)
                StartCoroutine(AnimateButton(b.gameObject, false));
        }

        foreach (RaycastResult result in results)
        {
            if(result.gameObject.tag == "TroopButton")
            {
                if(!result.gameObject.GetComponent<buttonAnimation>().isMoving)
                {
                    result.gameObject.GetComponent<buttonAnimation>().isMoving = true;
                    if (result.gameObject.GetComponent<buttonAnimation>().up) StartCoroutine(AnimateButton(result.gameObject, false));
                    else StartCoroutine(AnimateButton(result.gameObject, true));
                }
            }
            Debug.Log(result.gameObject.name);
        }
        
        
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

    public IEnumerator AnimateButton(GameObject button, bool up)
    {
        if (up)
        {
            float yPos = Mathf.Lerp(button.GetComponent<RectTransform>().anchoredPosition.y, -76, 0.2f);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x, yPos);
            if(button.GetComponent<RectTransform>().anchoredPosition.y <= -121.5) yield return null;
        }
        else
        {
            float yPos = Mathf.Lerp(button.GetComponent<RectTransform>().anchoredPosition.y, -185, 0.2f);
            button.GetComponent<RectTransform>().anchoredPosition = new Vector2(button.GetComponent<RectTransform>().anchoredPosition.x, yPos);
            if (button.GetComponent<RectTransform>().anchoredPosition.y >= -174.5) yield return null;
        }
        button.GetComponent<buttonAnimation>().isMoving = false;
    }
}
