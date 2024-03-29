﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Mono.Data.Sqlite;
using UnityEngine;

public class MainControler : MonoBehaviour
{
    void Start () {

        // Create database
        
    }
    
    private string userName;
    
    private string[] orderOfPointsSquare;
    private int numberOfLinesSquare;
    private float totalTimeSquare;
    private float[] partialTimesSquare;

    private float firstHalfTimeTap;
    private float secondHalfTimeTap;
    private float totalTimeTap;

    private float firstHalfTimeTapIdentification;
    private float secondHalfTimeTapIdentification;
    private float totalTimeTapIdentification;
    
    private string[] orderOfPointsCross;
    private float totalTimeCross;
    private float[] partialTimesCross;
    
    private string[] orderOfPointsSquareIdentification;
    private int numberOfLinesSquareIdentification;
    private float totalTimeSquareIdentification;
    private float[] partialTimesSquareIdentification;
    
    private string[] orderOfPointsCrossIdentification;
    private float totalTimeCrossIdentification;
    private float[] partialTimesCrossIdentification;
    
    private static MainControler _instance;

    private List<DatabaseObject> _databaseObjects = new List<DatabaseObject>();
    public static MainControler Instance 
    { 
        get { return _instance; } 
    } 
    private void Awake() 
    { 
        if (_instance != null && _instance != this) 
        { 
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void Test()
    {
    }

    public void GetName(string name)
    {
        userName = name;
    }

    //Metody zapisujące zebrane wartości do określonych pól
    public void SquareUserData(string[] order, int number, float wholeTime, float[] timeBetweenPoints)
    {
        orderOfPointsSquare = order;
        numberOfLinesSquare = number;
        totalTimeSquare = wholeTime;
        partialTimesSquare = timeBetweenPoints;
    }

    public void TapUserData(float _firstHalfTimeTap, float _secondHalfTimeTap, float _totalTimeTap)
    {
        firstHalfTimeTap = _firstHalfTimeTap;
        secondHalfTimeTap = _secondHalfTimeTap;
        totalTimeTap = _totalTimeTap;
    }

    public void CrossUserData(string[] order, float wholeTime, float[] timeBetweenPoints)
    {
        orderOfPointsCross = order;
        totalTimeCross = wholeTime;
        partialTimesCross = timeBetweenPoints;
    }

  
    public void IdentificationSquare(string[] order, int number, float wholeTime, float[] timeBetweenPoints)
    {
        orderOfPointsSquareIdentification = order;
        numberOfLinesSquareIdentification = number;
        totalTimeSquareIdentification = wholeTime;
        partialTimesSquareIdentification = timeBetweenPoints;
    }
    
    public void IdentificationCross(string[] order, float wholeTime, float[] timeBetweenPoints)
    {
        orderOfPointsCrossIdentification = order;
        totalTimeCrossIdentification = wholeTime;
        partialTimesCrossIdentification = timeBetweenPoints;
    }

    internal void IdentificationTap(float _firstHalfTimeTapIdentification, float _secondHalfTimeTapIdentification, float v)
    {
        firstHalfTimeTapIdentification = _firstHalfTimeTapIdentification;
        secondHalfTimeTapIdentification = _secondHalfTimeTapIdentification;
        totalTimeTapIdentification = v;
    }

    //Zapisanie zebranych wartości do bazy danych
    public void AddToDatabase()
    {
        ReadData();
        var user = new DatabaseObject(userName, orderOfPointsSquare, numberOfLinesSquare, totalTimeSquare, partialTimesSquare, orderOfPointsCross, totalTimeCross, partialTimesCross, firstHalfTimeTap, secondHalfTimeTap, totalTimeTap);
        
        _databaseObjects.Add(user);
        saveUserData();
    }
   
    //Pobranie ścieżki pliku zawierającego bazę danych
    private string GetDataFilePath() => Application.persistentDataPath + "/" + "datafile3";
    public void ReadData()
    {
        if (!File.Exists(GetDataFilePath()))
        {
            saveUserData();
        }
        using (Stream stream = File.Open(GetDataFilePath(), FileMode.Open))
        {
            BinaryFormatter bin = new BinaryFormatter();
            _databaseObjects = (List<DatabaseObject>)bin.Deserialize(stream);
        }
    }
    
    public void saveUserData()
    {
        using (Stream stream = File.Open(GetDataFilePath(), FileMode.Create))
        {
            BinaryFormatter bin = new BinaryFormatter();
            bin.Serialize(stream, _databaseObjects);
        }
    }

    public string Check()
    {
        ReadData();

        string result = "Nie ma takiej osoby";
        
        int correct = 0;
        int correctCross = 0;
        int correctTap = 0;
        float errorTime = 5.0f;
        List<DatabaseObject> possibleObjects = new List<DatabaseObject>();

        foreach (var person in _databaseObjects)
        {
            correct = 0;
            correctCross = 0;
            correctTap = 0;
            for (int i = 0; i < orderOfPointsSquareIdentification.Length; i++)
            {
               
                if (orderOfPointsSquareIdentification[i] == person.orderOfPointsSquare[i])
                {
                    correct++;
                }
            }

            if (Math.Abs(firstHalfTimeTapIdentification - person.firstHalfTimeTap) < errorTime)
                correctTap++;
            if (Math.Abs(secondHalfTimeTapIdentification - person.secondHalfTimeTap) < errorTime)
                correctTap++;

            for (int i = 0; i < orderOfPointsCrossIdentification.Length; i++)
            {
               
                if (orderOfPointsCrossIdentification[i] == person.orderOfPointsCross[i])
                {
                    correctCross++;
                }
            }
            
            if (correct == orderOfPointsSquareIdentification.Length && correctTap == 2 && correctCross==orderOfPointsCrossIdentification.Length)
            {
                possibleObjects.Add(person);
            }
        }
        
        if (possibleObjects.Count != 0)
        {
            foreach (var possiblePerson in possibleObjects)
            {
                possiblePerson.timeDifferenceSquare = Mathf.Abs(totalTimeSquareIdentification - possiblePerson.totalTimeSquare);
                possiblePerson.timeDifferenceTap = Mathf.Abs(totalTimeTapIdentification - possiblePerson.totalTimeTap);
                possiblePerson.timeDifferenceCross = Mathf.Abs(totalTimeCrossIdentification - possiblePerson.totalTimeCross);

                possiblePerson.precentageSquare =
                    CountPrecentage(possiblePerson.totalTimeSquare, possiblePerson.timeDifferenceSquare);
                possiblePerson.precentageCross =
                    CountPrecentage(possiblePerson.totalTimeCross, possiblePerson.timeDifferenceCross);
                possiblePerson.precentageTap =
                    CountPrecentage(possiblePerson.totalTimeTap, possiblePerson.timeDifferenceTap);

                possiblePerson.wholePrecentage = possiblePerson.precentageSquare + possiblePerson.precentageCross +
                                                 possiblePerson.precentageTap;
                
                Debug.Log("Total time tap for " + possiblePerson.userName +": "+  possiblePerson.totalTimeTap);

            }
            
            /*
            DatabaseObject lowestSquare = possibleObjects[0];
            DatabaseObject lowestTap = possibleObjects[0];
            DatabaseObject lowestCross = possibleObjects[0];
        
            foreach(var x in possibleObjects){
                if(x.timeDifferenceSquare < lowestSquare.timeDifferenceSquare)
                    lowestSquare = x;
            }


                possiblePerson.precentageSquare =
                    CountPrecentage(possiblePerson.totalTimeSquare, possiblePerson.timeDifferenceSquare);
                possiblePerson.precentageCross =
                    CountPrecentage(possiblePerson.totalTimeCross, possiblePerson.timeDifferenceCross);
                possiblePerson.precentageTap =
                    CountPrecentage(possiblePerson.totalTimeTap, possiblePerson.timeDifferenceTap);

                possiblePerson.wholePrecentage = possiblePerson.precentageSquare + possiblePerson.precentageCross +
                                                 possiblePerson.precentageTap;
                
                Debug.Log("Total time tap for " + possiblePerson.userName +": "+  possiblePerson.totalTimeTap);

            Debug.Log("SPRAWDZENIE RESULTATOW");
            Debug.Log(lowestSquare.userName);
            Debug.Log(lowestCross.userName);
            Debug.Log(lowestTap.userName);
            
            if (lowestSquare == lowestCross && lowestSquare == lowestTap)
            {
                result = lowestCross.userName;
            }
            */
            
            }
                        

            DatabaseObject lowestPrecentage = possibleObjects[0];
        
            foreach(var x in possibleObjects){
                if(x.wholePrecentage < lowestPrecentage.wholePrecentage)
                    lowestPrecentage = x;
            }
            
            result = lowestPrecentage.userName;
        }

        return result;
    }

    public float CountPrecentage(float totalTime, float timeDifference)
    {
        float precentage = (timeDifference / totalTime)*100;
        return precentage;
    }
}
