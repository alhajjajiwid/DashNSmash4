using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TapToHit : MonoBehaviour
{

    public GameObject destuctionParticles;
    public AudioSource destroySfx;
    public AudioSource hitSfx;
        
    private void OnMouseDown()
    {
        destroySfx.Play();
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
                hitSfx.Play();
            }
            Instantiate(destuctionParticles, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

}
