using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrder : MonoBehaviour
{

    public delegate void wasClicked();
    public wasClicked orderCollected;

    private void OnMouseDown()
    {
        if (GameController.Instance.carController.carIsStopped)
        {
            orderCollected();

            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
