using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoStart : MonoBehaviour
{
  
    void Start()
    {
        GameController.Instance.StartGame();
    }

   
}
