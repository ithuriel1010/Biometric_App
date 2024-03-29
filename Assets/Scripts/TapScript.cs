﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapScript : MonoBehaviour
{
    private float firstTimeTap = 0.0f;
    private float secondTimeTap = 0.0f;
    private bool thirdTimeTap = false;

    private float firstHalfTime = 0.0f;
    private float secondHalfTime = 0.0f;

    private ControlerScript _controlerScript;

    public void Start()
    {
        _controlerScript = FindObjectOfType<ControlerScript>();
    }

    //Metoda trzykrotnie zapisująca czas w którym przycisk został tapnięty
    public void SaveTimeOfTap()
    {
        if(firstTimeTap == 0.0f) {
            firstTimeTap = Time.time;
        } else if (secondTimeTap == 0.0f) {
            secondTimeTap = Time.time;
        } else if (thirdTimeTap == false) {
            secondHalfTime = Time.time - secondTimeTap;
            firstHalfTime = secondTimeTap - firstTimeTap;
            
            thirdTimeTap = true;
        }
    }

    //Przejście do kolejnego poziomu
    public void MoveToLevel3()
    {
        Debug.Log(thirdTimeTap);
        Debug.Log(firstHalfTime);
        Debug.Log(secondHalfTime);
        if(thirdTimeTap == true)
        {
            _controlerScript.TapUserData(firstHalfTime, secondHalfTime, Time.time - firstTimeTap);
            _controlerScript.EndOfTapLevel();
            SceneManager.LoadScene("Cross", LoadSceneMode.Single);
        }
    }
    
    //Przejście do kolejnego poziomu w przypadku identyfikacji
    public void MoveToIdentifyLevel3()
    {
        if (thirdTimeTap == true)
        {
            _controlerScript.TapIdentification(firstHalfTime, secondHalfTime, Time.time - firstTimeTap);
            SceneManager.LoadScene("IdentifyCross", LoadSceneMode.Single);
        }
    }
}
