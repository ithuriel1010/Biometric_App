using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControler : MonoBehaviour
{
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
}
