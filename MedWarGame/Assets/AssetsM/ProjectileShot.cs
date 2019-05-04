using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShot : MonoBehaviour
{
   
    [HideInInspector] public Vector3 forward = new Vector3();

    [SerializeField]
    private float _speed;
    private float cooldown = 2f;

   
    private Vector3 _forward = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        
      
    }
    
    void Update()
    {
        if (_forward != Vector3.zero)
        {

            this.transform.position += _forward * _speed;

            Invoke("DestroyThis", 0.6f);
        }
        

        

        /*
        if (cooldown >= 0)
        {
            this.transform.position += forward.normalized * Time.fixedDeltaTime;
            cooldown -= Time.fixedDeltaTime;
        }
        else Destroy(this);
        */
    }

    public void MoveTo(Vector3 pos)
    {
        Debug.Log(pos.ToString());
        _forward = pos;
    }
    public void DestroyThis()
    {
        this.gameObject.SetActive(false);
    }
}