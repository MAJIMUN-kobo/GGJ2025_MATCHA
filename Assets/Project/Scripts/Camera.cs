using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // set initial position
        transform.position = new Vector3(0, 1.9f, -0.3f);
        transform.rotation = Quaternion.Euler(75, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
