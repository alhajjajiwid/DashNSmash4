using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera Cam01;
    void Start()
    {
        Cam01 = Camera.main; 
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.LookAt(Cam01.transform);

        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
        
    }
}
