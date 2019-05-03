using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float energyPerSecond = 1f;

    public bool isPlayer = true;
    public bool isSelected;
    public GameObject radius;
    private Vector3 radiusTargetScale;

    private void Start()
    {
        radiusTargetScale = radius.transform.localScale;
        radius.transform.localScale = Vector3.zero;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isSelected)
                isSelected = false;
            else
                isSelected = true;
        }

        if (isSelected)
            radius.transform.localScale = Vector3.Lerp(radius.transform.localScale, radiusTargetScale, 0.1f);
        else
            radius.transform.localScale = Vector3.Lerp(radius.transform.localScale, Vector3.zero, 0.1f);
    }


    public float GetEnergy()
    {
        return energyPerSecond;
    }
}
