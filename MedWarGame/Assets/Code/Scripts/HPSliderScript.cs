using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSliderScript : MonoBehaviour
{
    [SerializeField] private GameObject treeObject = default;
    private float initialHP;
    private float treeHP;
    private Slider sli;

    private treeScript tree;
    public Image img;
    // Start is called before the first frame update
    void Start()
    {
        tree = treeObject.GetComponent<treeScript>();
        sli = GetComponent<Slider>();
        initialHP = tree.GetHp();
    }

    // Update is called once per frame
    void Update()
    {
        float currentHP = tree.GetHp();
        sli.value = currentHP / initialHP;

        if (currentHP <= 0)
        {
            Destroy(img);
        }
    }
}
