using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleEnabler : MonoBehaviour
{
    private void OnEnable()
    {
        this.gameObject.GetComponent<ParticleSystem>().Play();

    }
}
