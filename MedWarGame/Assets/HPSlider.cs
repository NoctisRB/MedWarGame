using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private treeScript tree;
    private Slider sli;

    private float initialHP;
    private float currentHP;

    public Image img;

    void Start()
    {
        sli = GetComponent<Slider>();
        initialHP = tree.GetHp();
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = tree.GetHp();
        sli.value = currentHP / initialHP;
        if (currentHP <= 0) Destroy(img);
    }
}
