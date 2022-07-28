using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TapToHit : MonoBehaviour
{

    public GameObject destuctionParticles;
        
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CarController dolly = other.GetComponent<CarController>();
            if (dolly.IsBoosted == false)
            {
                dolly.SlowDownCar();
            }
            Instantiate(destuctionParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
