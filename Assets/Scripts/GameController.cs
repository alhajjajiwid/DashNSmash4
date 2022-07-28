using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;


    public float Temperature;
    public float temperatureReduceAmmount = 0.1f;
    public float boostCooldownDuration = 7f;

    public CarController carController;


    public Image temperatureImage => UIController.instance.temperatureImage;

    public Image temperatureBG => UIController.instance.temperatureBG;
    public Button boostButton => UIController.instance.boostButton;
    public Gradient tempColor => UIController.instance.tempColor;

    public GameObject orderCancelled => UIController.instance.orderCancelled;
    public bool collectedOrder;
    public GameObject orderPickUp => UIController.instance.orderPickUp;
    public GameObject orderDelivery => UIController.instance.orderDelivery;


    public float totalPlayerMoney;
    public int currentJobValue;
    public Text moneyTxt => UIController.instance.moneyTxt;
    public Text totalTxt => UIController.instance.totalTxt;



    public GameObject shopPanel => UIController.instance.shopPanel;
    public Button boostBuyButton => UIController.instance.boostBuyButton;
    public Button thermalbagBuyButton => UIController.instance.thermalbagBuyButton;
    public Text shopPanelMoney => UIController.instance.shopPanelMoney;
    public int thermalBagcost = 400;
    public int boostcost = 1000;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        PauseGame();

        totalPlayerMoney = PlayerPrefs.GetFloat("Money");
    }

    public void StartGame()
    {
        shopPanel.SetActive(false);
        ResumeGame();
    }

    private void OnLevelWasLoaded(int level)
    {
        ResetDeliveryState();
        carController = FindObjectOfType<CarController>();
    }


    public void BuyThermalBag()
    {

        if (totalPlayerMoney >= thermalBagcost)
        {
            totalPlayerMoney -= thermalBagcost;
            temperatureReduceAmmount = 0.05f;
            thermalbagBuyButton.interactable = false;
            Debug.Log("Bought thermal bag");
        }
        else
        {
            // ui warning that you cant afford it
            Debug.Log("Can't afford thermal bag");
        }
    }

    public void BuyBoost()
    {
        if (totalPlayerMoney >= boostcost)
        {
            totalPlayerMoney -= boostcost;
            boostButton.gameObject.SetActive(true);
            boostBuyButton.interactable = false;
            Debug.Log("Bought boost");
        }
        else
        {
            // ui warning that you cant afford it
            Debug.Log("Can't afford boost");
        }
        
    }


    public void CollectOrder()
    {
        collectedOrder = true;
        Temperature = 1f;
        
        //orderPickUp.gameObject.SetActive(true);

    }

    public IEnumerator BoostStarted()
    {
        boostButton.interactable = false;
        boostButton.image.color = new Color(1, 1, 1, 0.5f);

        yield return new WaitForSeconds(boostCooldownDuration);

        boostButton.interactable = true;
        boostButton.image.color = new Color(1, 1, 1, 1);
    }

    

    private void Update()
    {
        if (UIController.instance == null)
            return;

        if (collectedOrder)
        {
            //temperatureSlider.gameObject.SetActive(true);
            temperatureImage.gameObject.SetActive(true);
            temperatureBG.gameObject.SetActive(true);

            Temperature -= temperatureReduceAmmount * Time.deltaTime;
            //temperatureSlider.value = Temperature;
            temperatureImage.fillAmount = Temperature;

            //temperatureSliderImage.color = tempColor.Evaluate(Temperature);
            temperatureImage.color = tempColor.Evaluate(Temperature);
            
        }
        else
        {
            //temperatureSlider.gameObject.SetActive(false);
            temperatureImage.gameObject.SetActive(false);
            temperatureBG.gameObject.SetActive(false);

        }

        shopPanelMoney.text = totalPlayerMoney.ToString();

       FailedToDeliver();
    }

    public void ResetDeliveryState()
    {
        Temperature = 1;
        collectedOrder = false;
    }

    public void FailedToDeliver()
    {
        if (Temperature <= 0f && collectedOrder == true)
        {
            orderCancelled.gameObject.SetActive(true);

            PauseGame();
        }
    }

    public void OrderDelivered()
    {
        collectedOrder = false;

        currentJobValue = Mathf.RoundToInt(200 * Temperature);
        Debug.Log("Delivered order: temperature was " + Temperature + " job value: " + currentJobValue);
        moneyTxt.text = currentJobValue.ToString();

        totalPlayerMoney += currentJobValue;
        PlayerPrefs.SetFloat("Money", totalPlayerMoney);
        totalTxt.text = totalPlayerMoney.ToString();

        orderDelivery.gameObject.SetActive(true);
    }

    public void ResetGame()
    {
        PlayerPrefs.SetFloat("Money", 0);
        PlayerPrefs.SetInt("PlayedFirstLevel", 0);
    }



    void PauseGame()
    {
        Time.timeScale = 0;
    }

    void ResumeGame()
    {
        Time.timeScale = 1;
    }


    public void RestartLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }

    public void LoadNextScene()
    {
        bool hasPlayedFirstLevelBefore;

        if(PlayerPrefs.GetInt("PlayedFirstLevel") == 1)
        {
            hasPlayedFirstLevelBefore = true;
        }
        else
        {
            hasPlayedFirstLevelBefore = false;
        }


        int currentScene = SceneManager.GetActiveScene().buildIndex;

        //if (hasPlayedFirstLevelBefore && currentScene == 0)
        //{
        //    SceneManager.LoadScene(currentScene + 2);
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("PlayedFirstLevel", 1);
            SceneManager.LoadScene(currentScene + 1);
        //}
    }
}
