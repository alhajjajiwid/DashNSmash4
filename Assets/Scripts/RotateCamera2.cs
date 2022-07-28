using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera2 : MonoBehaviour
{
    public GameObject cam02;
    public GameObject cam03;

    public void OnTriggerEnter(Collider other)
    {
        cam02.gameObject.SetActive(false);
        cam03.gameObject.SetActive(true);
    }
}
