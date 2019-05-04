using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform selectedPlanet;
    public static bool isPlanetSelected = false;
    private Transform originalPos;

    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private GameObject ExitButton;
    [SerializeField] private GameObject BackButton;

    [SerializeField] public AudioManager audioManager;

    void Start()
    {
        originalPos = GameObject.Find("CameraMainPos").transform;
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BackgroundMenu");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPlanetSelected && !MainMenuCanvasScript.playerIsInSettings)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Player")
                {
                    selectedPlanet = GameObject.Find("CameraPos" + hit.collider.name).transform;
                    isPlanetSelected = true;
                    BackButton.SetActive(true);
                    SettingsButton.SetActive(false);
                    ExitButton.SetActive(false);
                }                
            }
        }

        if (!isPlanetSelected)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalPos.rotation, 0.02f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPlanetSelected) {
            DeselectPlanet();
        }            

        if (selectedPlanet != null)
        {
            transform.position = Vector3.Lerp(transform.position, selectedPlanet.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, selectedPlanet.rotation, 0.02f);
            audioManager.Play("CameraMenuZoom");
        }
    }
    public void DeselectPlanet()
    {
        isPlanetSelected = false;
        selectedPlanet = null;
        SettingsButton.SetActive(true);
        ExitButton.SetActive(true);
        BackButton.SetActive(false);
    }
}
