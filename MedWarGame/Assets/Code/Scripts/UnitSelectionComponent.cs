using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class UnitSelectionComponent : MonoBehaviour
{
    bool isSelecting = false;
    Vector3 mousePosition1;

    public GameObject selectionCirclePrefab;

    private List<GameObject> _selectedObjects = new List<GameObject>();


    void Update()
    {
        
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                GameObject objectHit = hit.transform.gameObject;
                Debug.Log(objectHit.name);
                if (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "EnemyBase")
                {
                    
                    if (_selectedObjects != null)
                    {
                        //Debug.Log("Did Hit");
                        MoveUnits(hit.transform.gameObject, _selectedObjects);
                    }

                }
            }
            
            
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (selectableObject.selectionCircle != null)
                {
                    //Destroy(selectableObject.selectionCircle.gameObject);
                   
                   
                    selectableObject.selectionCircle.SetActive(true);
                }
            }
        }
        // If we let go of the left mouse button, end selection
        if (Input.GetMouseButtonUp(0))
        {
            var selectedObjects = new List<SelectableUnitComponent>();
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {

                    _selectedObjects.Add(selectableObject.gameObject);
                    selectableObject.selectionCircle.SetActive(true);
                    
                }
            }



            var sb = new StringBuilder();
            sb.AppendLine(string.Format("Selecting [{0}] Units", selectedObjects.Count));
            foreach (var selectedObject in selectedObjects)
                sb.AppendLine("-> " + selectedObject.gameObject.name);
            Debug.Log(sb.ToString());

            isSelecting = false;
        }

        // Highlight all objects within the selection box
        if (isSelecting)
        {
            foreach (var selectableObject in FindObjectsOfType<SelectableUnitComponent>())
            {
                if (IsWithinSelectionBounds(selectableObject.gameObject))
                {
                    if (selectableObject.selectionCircle == null)
                    {
                        selectableObject.selectionCircle.SetActive(true);
                        /*
                        selectableObject.selectionCircle = Instantiate( selectionCirclePrefab );
                        selectableObject.selectionCircle.transform.SetParent( selectableObject.transform, false );
                        selectableObject.selectionCircle.transform.eulerAngles = new Vector3( 90, 0, 0 );*/
                        
                            _selectedObjects.Add(selectableObject.gameObject);
                        
                    }
                }
                else
                {
                    if (selectableObject.selectionCircle != null)
                    {
                        //Destroy(selectableObject.selectionCircle.gameObject);
                        selectableObject.selectionCircle.SetActive(false);
                    }
                }
            }
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    void OnGUI()
    {
        if (isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    private void MoveUnits(GameObject target, List<GameObject> selectedObjects)
    {

        foreach (var selectedUnit in selectedObjects)
        {
            selectedUnit.GetComponent<troopScript>().MoveTo(target);
        }
        if (_selectedObjects != null)
        {
            _selectedObjects.Clear();
        }
    }
}