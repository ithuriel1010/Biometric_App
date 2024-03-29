﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Point : MonoBehaviour
{
    string[] pointsOrder { get; set; } = new string[7];
    public int order = 0;
    private bool onEnter = false;

    private ControlerScript _controlerScript;
    private void Start()
    {
        _controlerScript = FindObjectOfType<ControlerScript>();
    }
    private void Update()
    {        

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (hit != null && hit.collider != null)
            {                    
                _controlerScript.Points(hit.collider.name);  
            }
        }

    }

    public void MoveToLevel2()
    {
        _controlerScript.CountWholeTime();
        _controlerScript.CountTimeBetweenPoints();
        _controlerScript.EndOfSquareLevel();
        //Debug.Log("Change scene");
        SceneManager.LoadScene("Tap", LoadSceneMode.Single);
    }
    
    public void MoveToIdentifyLevel2()
    {
        _controlerScript.CountWholeTime();
        _controlerScript.CountTimeBetweenPoints();
        _controlerScript.EndOfSquareIdentification();
        //Debug.Log("Change scene");
        SceneManager.LoadScene("IdentifyTap", LoadSceneMode.Single);
    }

    public void EndAddingPerson()
    {
         _controlerScript.CountWholeTime();
         _controlerScript.CountTimeBetweenPoints();
         _controlerScript.EndOfCrossLevel();

        SceneManager.LoadScene("mainScreen", LoadSceneMode.Single);
    }

    public void EndIdentification()
    {
        _controlerScript.CountWholeTime();
        _controlerScript.CountTimeBetweenPoints();
        _controlerScript.EndOfCrossIdentification();
        
        SceneManager.LoadScene("Result", LoadSceneMode.Single);
    }
}
