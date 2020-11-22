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
        Debug.Log("LOL");
    }

    public void GetName(string name)
    {
        userName = name;
    }
    public void SquareUserData(string[] order, int number, float wholeTime, float[] timeBetweenPoints)
    {
        orderOfPointsSquare = order;
        numberOfLinesSquare = number;
        totalTimeSquare = wholeTime;
        partialTimesSquare = timeBetweenPoints;
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

    public string CountPrecentage()
    {
        int correctOrder = 0;
        for (int i = 0; i < orderOfPointsSquare.Length; i++)
        {
            if (orderOfPointsSquare[i] == orderOfPointsSquareIdentification[i])
            {
                correctOrder++;
            }
        }

        if (correctOrder == orderOfPointsSquare.Length)
        {
            return "Correct order";
        }
        else
        {
            return "Incorrect order";
        }
    }

    public void AddToDatabase()
    {
        ReadData();
        //'{userName}', '{orderOfPointsSquare[0]}', '{orderOfPointsSquare[1]}', '{orderOfPointsSquare[2]}', '{orderOfPointsSquare[3]}', '{orderOfPointsSquare[4]}', {totalTimeSquare}, '{orderOfPointsCross[0]}', '{orderOfPointsCross[1]}', '{orderOfPointsCross[2]}', '{orderOfPointsCross[3]}', '{orderOfPointsCross[4]}', '{orderOfPointsCross[5]}', '{orderOfPointsCross[6]}', {totalTimeCross})";
        var user = new DatabaseObject(userName, orderOfPointsSquare, numberOfLinesSquare, totalTimeSquare, partialTimesSquare, orderOfPointsCross, totalTimeCross, partialTimesCross);
        
        _databaseObjects.Add(user);
        saveUserData();
    }

    private string GetDataFilePath() => Application.persistentDataPath + "/" + "datafile";
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
        List<DatabaseObject> possibleObjects = new List<DatabaseObject>();

        foreach (var person in _databaseObjects)
        {
            correct = 0;
            correctCross = 0;
            for (int i = 0; i < orderOfPointsSquareIdentification.Length; i++)
            {
               
                if (orderOfPointsSquareIdentification[i] == person.orderOfPointsSquare[i])
                {
                    correct++;
                }
            }
            
            for (int i = 0; i < orderOfPointsCrossIdentification.Length; i++)
            {
               
                if (orderOfPointsCrossIdentification[i] == person.orderOfPointsCross[i])
                {
                    correctCross++;
                }
            }
            
            if (correct == orderOfPointsSquareIdentification.Length && correctCross==orderOfPointsCrossIdentification.Length)
            {
                possibleObjects.Add(person);
            }
        }
        
        //float[] timeDifference = new float[possibleObjects.Count];

        if (possibleObjects.Count != 0)
        {
            foreach (var possiblePerson in possibleObjects)
            {
                possiblePerson.timeDifferenceSquare = Mathf.Abs(totalTimeSquareIdentification - possiblePerson.totalTimeSquare);
                possiblePerson.timeDifferenceCross = Mathf.Abs(totalTimeCrossIdentification - possiblePerson.totalTimeCross);
            }
            
            DatabaseObject lowestSquare = possibleObjects[0];
            DatabaseObject lowestCross = possibleObjects[0];
        
            foreach(var x in possibleObjects){
                if(x.timeDifferenceSquare < lowestSquare.timeDifferenceSquare)
                    lowestSquare = x;
            }
        
            foreach(var x in possibleObjects){
                if(x.timeDifferenceCross < lowestCross.timeDifferenceCross)
                    lowestCross = x;
            }

            if (lowestSquare == lowestCross)
            {
                result = lowestCross.userName;
            }
        }

        return result;
    }
}
