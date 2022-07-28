using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RotateCamera : MonoBehaviour
{
    public GameObject cam01;
    public GameObject cam02;
    

    public void OnTriggerEnter(Collider other)
    {
        cam01.gameObject.SetActive(false);
        cam02.gameObject.SetActive(true);
    }
}
