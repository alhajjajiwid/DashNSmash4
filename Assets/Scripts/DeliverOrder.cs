using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverOrder : MonoBehaviour
{
    public delegate void wasClicked();
    public wasClicked orderDelivered;

    private void OnMouseDown()
    {
        orderDelivered();

        gameObject.GetComponent<Collider>().enabled = false;
    }
}
