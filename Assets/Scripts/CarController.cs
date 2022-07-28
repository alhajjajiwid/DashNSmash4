using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CarController : MonoBehaviour
{
    public float normalSpeed = 10f;
    private float reverseSeconds = 0.1f;
    public float boostedSpeed = 30f;
    public float acceleration = 10f;
    public float boostTime = 3f;

    public GameObject particleEffects, boostParticles;

    public CinemachineDollyCart dolly;

    public bool carIsStopped;

    public bool IsBoosted;

    public void BoostCar()
    {
        StartCoroutine(BoostOverTime(boostTime));
        StartCoroutine(GameController.Instance.BoostStarted());
    }

    private IEnumerator BoostOverTime(float boostTime)
    {
        IsBoosted = true;
        boostParticles.SetActive(true);

        yield return StartCoroutine(ChangeSpeed(boostedSpeed, acceleration));

        yield return new WaitForSeconds(boostTime);

        yield return StartCoroutine(ChangeSpeed(normalSpeed, acceleration));

        boostParticles.SetActive(false);
        IsBoosted = false;
    }

    private IEnumerator ChangeSpeed(float newSpeed, float changeRate)
    {
        bool speedIncreasing = newSpeed > dolly.m_Speed;

        do
        {
            dolly.m_Speed = Mathf.Lerp(dolly.m_Speed, newSpeed, changeRate * Time.deltaTime);
            yield return null;

        } while (carIsStopped == false && (speedIncreasing && dolly.m_Speed < newSpeed) || (!speedIncreasing && dolly.m_Speed > newSpeed));
    }



    public void SlowDownCar()
    {
        StartCoroutine(CSlowDownCar());
    }

    public IEnumerator CSlowDownCar()
    {
        particleEffects.SetActive(true);
        dolly.m_Speed = -5f;

        yield return new WaitForSeconds(reverseSeconds);
        
        float acceleration = 0.01f;
        while (dolly.m_Speed < normalSpeed - 1)
        {
            dolly.m_Speed = Mathf.Lerp(dolly.m_Speed, normalSpeed, acceleration);
            yield return null;
        }

        particleEffects.SetActive(false);
        dolly.m_Speed = normalSpeed;
    }

}
