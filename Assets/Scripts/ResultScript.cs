﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string result = MainControler.Instance.Check();        //Sprawdzenie wszystkich danych i wyświetlenie najbardziej prawdopodbnej osoby
        text.text = result;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainScreen", LoadSceneMode.Single);
    }
    
    void Update()
    {
        
    }
}
