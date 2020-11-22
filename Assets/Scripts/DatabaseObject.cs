using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DatabaseObject
{
    public string userName;
    
    public string[] orderOfPointsSquare;
    public int numberOfLinesSquare;
    public float totalTimeSquare;
    public float timeDifferenceSquare;
    public float[] partialTimesSquare;
    
    public string[] orderOfPointsCross;
    public float totalTimeCross;
    public float timeDifferenceCross;
    public float[] partialTimesCross;

    public DatabaseObject(string userName, string[] orderOfPointsSquare, int numberOfLinesSquare, float totalTimeSquare, float[] partialTimesSquare, string[] orderOfPointsCross, float totalTimeCross, float[] partialTimesCross)
    {
        this.userName = userName;
        this.orderOfPointsSquare = orderOfPointsSquare;
        this.numberOfLinesSquare = numberOfLinesSquare;
        this.totalTimeSquare = totalTimeSquare;
        this.partialTimesSquare = partialTimesSquare;
        this.orderOfPointsCross = orderOfPointsCross;
        this.totalTimeCross = totalTimeCross;
        this.partialTimesCross = partialTimesCross;
    }
}
