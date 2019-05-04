using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSize = 18f;
    public float minSize = 8f;
    public float maxX = -10f;
    public float minX = -32f;
    public float maxY = 1.7f;
    public float minY = -16.5f;

    public float smoothness = 22;

    private Vector3 movement = new Vector2();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Get input for horizontal
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) movement.x = smoothness;        
        else if (Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D)) movement.x = -smoothness;        
        else movement.x = 0;
        //Get input for vertical move
        if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) movement.y = smoothness;        
        else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W)) movement.y = -smoothness;        
        else movement.y = 0;
        //Moving and clamping
        transform.position = Vector3.Lerp(transform.position, transform.position += movement, 0.02f);
        float x = transform.position.x;
        float y = transform.position.y;
        if (x < minX) x = minX;
        else if (x > maxX) x = maxX;
        if (y < minY) y = minY;
        else if (y > maxY) y = maxY;
        transform.position = new Vector3(x, y, transform.position.z);

        float targetOrtographicSize = Camera.main.orthographicSize -= Input.mouseScrollDelta.y;
        if (targetOrtographicSize < minSize) targetOrtographicSize = minSize;
        if (targetOrtographicSize > maxSize) targetOrtographicSize = maxSize;
        Camera.main.orthographicSize = Mathf.SmoothStep(Camera.main.orthographicSize, targetOrtographicSize, 4f);
    }
    private void FixedUpdate()
    {
        
    }
}
