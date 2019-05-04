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
    private bool alphaIncresing = false;

    private void Start()
    {
        Debug.Log(radius.GetComponent<SpriteRenderer>().color.a);
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
        {
            radius.transform.localScale = Vector3.Lerp(radius.transform.localScale, radiusTargetScale, 0.1f);
            AlphaChanger();
        }
        else
            radius.transform.localScale = Vector3.Lerp(radius.transform.localScale, Vector3.zero, 0.1f);
    }
    public float GetEnergy()
    {
        return energyPerSecond;
    }
    private void AlphaChanger()
    {
        Color tempColor = radius.GetComponent<SpriteRenderer>().color;
        float tempColorAlpha = tempColor.a;
        if (tempColorAlpha >= 0.49f)            
            alphaIncresing = false;
        if (tempColorAlpha <= 0.31f)
            alphaIncresing = true;
        if (alphaIncresing)
            tempColorAlpha = Mathf.Lerp(tempColorAlpha, 0.5f, 0.05f);
            //tempColorAlpha += 0.6f * Time.deltaTime;
        else
            tempColorAlpha = Mathf.Lerp(tempColorAlpha, 0.3f, 0.05f);
            //tempColorAlpha -= 0.6f * Time.deltaTime;

        Debug.Log(tempColorAlpha);
        tempColor.a = tempColorAlpha;
        radius.GetComponent<SpriteRenderer>().color = tempColor;
    }
}
