using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectOrder : MonoBehaviour
{

    public delegate void wasClicked();
    public wasClicked orderCollected;
    public AudioSource playSound;
    public GameObject pickUpSign;

    private void OnMouseDown()
    {
        pickUpSign.SetActive(false);
        playSound.Play();

        if (GameController.Instance.carController.carIsStopped)
        {
            orderCollected();

            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
