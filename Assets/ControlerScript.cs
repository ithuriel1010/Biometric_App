using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlerScript : MonoBehaviour
{
    string[] pointsOrder { get; set; } = new string[15];
    private float[] timesOfPoints { get; set; } = new float[15];
    private int order = 0;
    private string currentTime;
    private int numberOfLines = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Debug.Log("XXX");
    }

    public void Points(string name)
    {
        if (order > 0)
        {
            if (pointsOrder[order-1] != name)
            {
                pointsOrder[order] = name;
                timesOfPoints[order] = Time.time;
                
                Debug.Log(timesOfPoints[order]);
                order++;

            }
        }
        else
        {
            pointsOrder[order] = name;
            timesOfPoints[order] = Time.time;
                
            Debug.Log(timesOfPoints[order]);
            order++;
        }
    }

    public void CountWholeTime()
    {
        //Debug.Log("0: "+timesOfPoints[0]+" ostatnie: "+ timesOfPoints[order-1]);
        float wholeTime = timesOfPoints[order-1] - timesOfPoints[0];

        Debug.Log((wholeTime));
    }

    public void CountTimeBetweenPoints()
    {
        var filteredTimes = timesOfPoints.Where(x => x != 0).ToArray();
        float[] timesBetweenPoints = new float[filteredTimes.Length-1];

        for (int i = 0; i < filteredTimes.Length-1; i++)
        {
            
            timesBetweenPoints[i] = filteredTimes[i + 1] - filteredTimes[i];
           
        }
        
        //Debug.Log(numberOfLines);
        //Debug.Log(timesBetweenPoints.Length);
        //Debug.Log("0: "+timesBetweenPoints[0]+" 1: "+timesBetweenPoints[1]+" ostatnie: "+timesBetweenPoints[timesBetweenPoints.Length-1]);
    }

    public void NewLine()
    {
        numberOfLines++;
    }
}
