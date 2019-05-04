using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShot : MonoBehaviour
{
    public GameObject bulletEmitter;
    [HideInInspector] public Vector3 forward = new Vector3();

    private float cooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = bulletEmitter.transform.position;
    }
    

    void FixedUpdate()
    {
        if (cooldown < 0)
        {
            this.transform.position += forward.normalized * Time.fixedDeltaTime;
            cooldown -= Time.fixedDeltaTime;
        }
        else Destroy(this);
    }
    
}
