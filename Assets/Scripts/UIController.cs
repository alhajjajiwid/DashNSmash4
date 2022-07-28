using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public Image temperatureImage;
    public Image temperatureBG;
    public Button boostButton;
    public Gradient tempColor;

    public GameObject orderCancelled;
    public GameObject orderPickUp;
    public GameObject orderDelivery;
    public Text moneyTxt;
    public Text totalTxt;
    public GameObject shopPanel;
    public Button boostBuyButton, thermalbagBuyButton;
    public Text shopPanelMoney;

    public static UIController instance;

    private void Awake()
    {
        instance = this;
        orderCancelled.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        GameController.Instance.StartGame();
    }

    public void TryAgain()
    {
        Debug.Log("TryAgain");
        orderCancelled.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BuyBoost()
    {
        GameController.Instance.BuyBoost();
    }

    public void BuyThermalBag()
    {
        GameController.Instance.BuyThermalBag();
    }

}
