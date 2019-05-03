using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform selectedPlanet;
    private bool isPlanetSelected = false;
    private Transform originalPos;

    void Start()
    {
        originalPos = GameObject.Find("CameraMainPos").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPlanetSelected)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.tag == "Player")
                {
                    selectedPlanet = GameObject.Find("CameraPos" + hit.collider.name).transform;
                    isPlanetSelected = true;
                }                
            }
        }

        if (!isPlanetSelected)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalPos.rotation, 0.02f);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isPlanetSelected) {
            isPlanetSelected = false;
            selectedPlanet = null;
        }            

        if (selectedPlanet != null)
        {
            transform.position = Vector3.Lerp(transform.position, selectedPlanet.position, 0.05f);
            transform.rotation = Quaternion.Lerp(transform.rotation, selectedPlanet.rotation, 0.02f);
        }
    }
}
