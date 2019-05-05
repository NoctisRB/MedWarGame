using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCameraScript : MonoBehaviour
{
    // Start is called before the first frame update    
    public static bool isPlanetSelected = false;
    private Transform originalPos;
    private int level = default;
    private Transform selectedPlanet;

    [SerializeField] private SpriteRenderer textPlanet1; //Level 1 -- Hit Space to play
    [SerializeField] private SpriteRenderer textPlanet2; //Locked
    [SerializeField] private SpriteRenderer textPlanet3; //Locked
    [SerializeField] private SpriteRenderer textPlanet4; //Locked
    [SerializeField] private SpriteRenderer textPlanet5; //Locked

    [SerializeField] private GameObject SettingsButton;
    [SerializeField] private GameObject ExitButton;
    [SerializeField] private GameObject BackButton;

    [SerializeField] public AudioManager audioManager;

    void Start()
    {
        Time.timeScale = 1;
        originalPos = GameObject.Find("CameraMainPos").transform;

        audioManager = FindObjectOfType<AudioManager>();
        audioManager.Play("BackgroundMenu");

        textPlanet1.color = new Color(textPlanet1.color.r, textPlanet1.color.g, textPlanet1.color.b, 0);
        textPlanet2.color = new Color(textPlanet1.color.r, textPlanet1.color.g, textPlanet1.color.b, 0);
        textPlanet3.color = new Color(textPlanet1.color.r, textPlanet1.color.g, textPlanet1.color.b, 0);
        textPlanet4.color = new Color(textPlanet1.color.r, textPlanet1.color.g, textPlanet1.color.b, 0);
        textPlanet5.color = new Color(textPlanet1.color.r, textPlanet1.color.g, textPlanet1.color.b, 0);
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
                    CheckWhichPlanetIsHit(hit.collider);
                }                
            }
        }

        if (!isPlanetSelected)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos.position, 0.05f * 60 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalPos.rotation, 0.02f * 60 * Time.deltaTime);            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPlanetSelected) {
            DeselectPlanet();
        }            

        if (selectedPlanet != null)
        {
            transform.position = Vector3.Lerp(transform.position, selectedPlanet.position, 0.05f * 60 * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, selectedPlanet.rotation, 0.02f * 60 * Time.deltaTime);
            audioManager.Play("CameraMenuZoom");
        }

        if (selectedPlanet)
        {
            if (level == 1)
            {
                StartCoroutine(Appear(textPlanet1, 1f));
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                    SceneManager.LoadScene("Level1");
            }
            else if (level == 2)
            {
                StartCoroutine(Appear(textPlanet2, 1f));
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                    SceneManager.LoadScene("Level2");
            }
            else if (level == 3)
            {
                StartCoroutine(Appear(textPlanet3, 1f));
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                    SceneManager.LoadScene("Level3");
            }
            else if (level == 4) StartCoroutine(Appear(textPlanet4, 1f));
            else if (level == 5) StartCoroutine(Appear(textPlanet5, 1f));
        }
    }

    public void DeselectPlanet()
    {
        Debug.Log(level);
        if (level == 1) StartCoroutine(Fade(textPlanet1, 1f));
        else if (level == 2) StartCoroutine(Fade(textPlanet2, 1f));
        else if (level == 3) StartCoroutine(Fade(textPlanet3, 1f));
        else if (level == 4) StartCoroutine(Fade(textPlanet4, 1f));
        else if (level == 5) StartCoroutine(Fade(textPlanet5, 1f));
        
        level = 0;
        isPlanetSelected = false;
        selectedPlanet = null;
        SettingsButton.SetActive(true);
        ExitButton.SetActive(true);
        BackButton.SetActive(false);
    }
  
    private void CheckWhichPlanetIsHit(Collider col)
    {
        if (col.name == "Planet1") level = 1;
        else if (col.name == "Planet2") level = 2;
        else if (col.name == "Planet3") level = 3;
        else if (col.name == "Planet4") level = 4;
        else if (col.name == "Planet5") level = 5;
    }
    IEnumerator Fade(SpriteRenderer text, float fadeTime)
    {
        do
        {
            float alpha = text.color.a;
            alpha = Mathf.Lerp(alpha, 0, 0.3f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
            fadeTime -= Time.deltaTime;
            yield return null;
        }
        while (fadeTime >= 0);               
    }
    IEnumerator Appear(SpriteRenderer text, float fadeTime)
    {
        do
        {
            float alpha = text.color.a;
            alpha = Mathf.Lerp(alpha, 1, 0.03f);
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
            fadeTime -= Time.deltaTime;
            yield return null;
        }
        while (fadeTime >= 0);
    }
}
