using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float energyPerSecond = 1f;

    public bool isPlayer = true;
    public bool isSelected;
    public GameObject radiusSprite;
    private Vector3 radiusTargetScale;
    private bool alphaIncresing = false;

    private void Start()
    {
        radiusTargetScale = radiusSprite.transform.localScale;
        radiusSprite.transform.localScale = Vector3.zero;
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
            radiusSprite.transform.localScale = Vector3.Lerp(radiusSprite.transform.localScale, radiusTargetScale, 0.1f);
            AlphaChanger();
        }
        else
            radiusSprite.transform.localScale = Vector3.Lerp(radiusSprite.transform.localScale, Vector3.zero, 0.1f);
    }
    public float GetEnergy()
    {
        return energyPerSecond;
    }

    private void AlphaChanger()
    {
        Color tempColor = radiusSprite.GetComponent<SpriteRenderer>().color;
        float tempColorAlpha = tempColor.a;
        if (tempColorAlpha >= 0.69f) alphaIncresing = false;
        if (tempColorAlpha <= 0.31f) alphaIncresing = true;
        if (alphaIncresing) tempColorAlpha = Mathf.Lerp(tempColorAlpha, 0.7f, 0.05f);
        else tempColorAlpha = Mathf.Lerp(tempColorAlpha, 0.3f, 0.05f);

        tempColor.a = tempColorAlpha;
        radiusSprite.GetComponent<SpriteRenderer>().color = tempColor;
    }
}
