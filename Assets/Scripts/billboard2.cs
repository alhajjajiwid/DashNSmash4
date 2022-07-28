using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboard2 : MonoBehaviour
{
    public Camera Cam02;
    void Start()
    {
        Cam02 = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.forward = Cam02.transform.forward;

    }
}
