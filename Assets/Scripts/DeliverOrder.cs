using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverOrder : MonoBehaviour
{
    public delegate void wasClicked();
    public wasClicked orderDelivered;
    public AudioSource deliverSFX;
    public GameObject deliverSign;

    private void OnMouseDown()
    {
        deliverSign.SetActive(false);
        deliverSFX.Play();

        orderDelivered();

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
