using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarStop : MonoBehaviour

{

    public CinemachineDollyCart dollyCart;
    public CollectOrder myOrder;
    public DeliverOrder delivery;
    public GameObject car;

    public float minSpeed = 0f;
    public float originalSpeed;

    public float decelaration = 10f;
    public int moneyValue;

    public AudioSource PlaySound;

    private void Awake()
    {
        myOrder = FindObjectOfType<CollectOrder>();
        delivery = FindObjectOfType<DeliverOrder>();
    }

    void RestartCar()
    {
        car.GetComponent<CarController>().carIsStopped = false;
        dollyCart.m_Speed = originalSpeed;

        if(myOrder != null)
        {
            myOrder.orderCollected -= RestartCar;
            

        }
        

        GameController.Instance.CollectOrder();
        car.GetComponent<CarController>().SlowDownCar();

    }
    public void OnTriggerEnter(Collider other)
    {
        car.GetComponent<CarController>().carIsStopped = true;
        StartCoroutine(StopCar());

        if(myOrder != null)
        {
            myOrder.orderCollected += RestartCar;
            
        }
        
        if(delivery != null)
        {
            delivery.orderDelivered += Deliver;
            

        }
        
    }

    public void Deliver()
    {
        StartCoroutine(StopCar());

        if(delivery != null)
        {
            delivery.orderDelivered -= Deliver;
        }

        GameController.Instance.OrderDelivered();
    }

    public IEnumerator StopCar()
    {
        do
        {

            dollyCart.m_Speed = Mathf.Lerp(dollyCart.m_Speed, minSpeed, decelaration * Time.deltaTime);
            yield return null;

        } while (dollyCart.m_Speed > minSpeed);
    }



}
