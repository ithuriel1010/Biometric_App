using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControlerScript : MonoBehaviour
{
    string[] pointsOrder { get; set; } = new string[7];
    private float[] timesOfPoints { get; set; } = new float[15];
    private int order = 0;
    private string currentTime;
    private int numberOfLines = 0;
    private float wholeTime;
    private float[] timesBetweenPoints;
    private float firstHalfTimeTap;
    private float secondHalfTimeTap;
    private float totalTimeTap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Points(string name)
    {
        if (order > 0)        //Jeśli nie jest to pierwszy dotknięty punkt
        {
            if (pointsOrder[order-1] != name)    //Jeżeli użytkownik wjechał na inny punkt niż był wcześniej (dzięki temu nie liczy np. że użytkownik dotknął punktu pierwszego ponad 50 razy)
            {
                pointsOrder[order] = name;
                timesOfPoints[order] = Time.time;
                
                //Debug.Log(timesOfPoints[order]);
                order++;

            }
        }
        else              //Jeśli jest to pierwszy dotknięty punkt to zapisz jego nazwę i zwiększ licznik
        {
            pointsOrder[order] = name;
            timesOfPoints[order] = Time.time;
                
            //Debug.Log(timesOfPoints[order]);
            order++;
        }
    }

    public void CountWholeTime()
    {
        wholeTime = (timesOfPoints[order-1] - timesOfPoints[0]);    //Obliczanie czasu jaki miną mniędzy dotknięciem pierwszego i ostatniego punktu
    }

    public void CountTimeBetweenPoints()        //Obliczenie czasu pomiędzy dotknięciem dwóch punktów (ostatecznie niewykorzystywane)
    {
        var filteredTimes = timesOfPoints.Where(x => x != 0).ToArray();
        timesBetweenPoints = new float[filteredTimes.Length-1];

        for (int i = 0; i < filteredTimes.Length-1; i++)
        {
            
            timesBetweenPoints[i] = filteredTimes[i + 1] - filteredTimes[i];
           
        }
        
    }

    //Przypisanie wartości do zmiennych
    public void TapUserData(float _firstHalfTimeTap, float _secondHalfTimeTap, float _totalTimeTap)
    {
        firstHalfTimeTap = _firstHalfTimeTap;
        secondHalfTimeTap = _secondHalfTimeTap;
        totalTimeTap = _totalTimeTap;
        //Debug.Log("Szybkosc total tapa");
        //Debug.Log(_totalTimeTap);
    }

    public void NewLine()
    {
        numberOfLines++;
    }

    public void EndOfSquareLevel()
    {
        var orderOfPointsClicked = pointsOrder.Where(x => x != null).ToArray();
        MainControler.Instance.SquareUserData(orderOfPointsClicked, numberOfLines, wholeTime, timesBetweenPoints);
        Debug.Log(timesOfPoints[2]);
    }

    //Wyświetlenie informacji w konsoli na temat poziomu 2
    public void EndOfTapLevel()
    {
        Debug.Log(firstHalfTimeTap);
        Debug.Log(secondHalfTimeTap);
        MainControler.Instance.TapUserData(firstHalfTimeTap, secondHalfTimeTap, totalTimeTap);
    }
    
    //Wyświetlenie informacji w konsoli na temat poziomu 3
    public void EndOfCrossLevel()
    {
        var orderOfPointsClicked = pointsOrder;
        MainControler.Instance.CrossUserData(orderOfPointsClicked, wholeTime, timesBetweenPoints);
        MainControler.Instance.AddToDatabase();
        Debug.Log(timesOfPoints[2]);

    }
    
    //Przekazanie wartości zebranych w poziomie "Kwadrat - identyfikacja" do głównej metody
    public void EndOfSquareIdentification()
    {
        var orderOfPointsClicked = pointsOrder.Where(x => x != null).ToArray();
        MainControler.Instance.IdentificationSquare(orderOfPointsClicked, numberOfLines, wholeTime, timesBetweenPoints);
    }
    //Przekazanie wartości zebranych w poziomie "Krzyż - identyfikacja" do głównej metody
    public void EndOfCrossIdentification()
    {
        var orderOfPointsClicked = pointsOrder;
        MainControler.Instance.IdentificationCross(orderOfPointsClicked, wholeTime, timesBetweenPoints);
    }

    //Przekazanie zebranych wartości do głównej metody
    public void TapIdentification(float firstHalfTime, float secondHalfTime, float v)
    {
        Debug.Log("Szybkosc identyfikacji tapa");
        Debug.Log(v);
        MainControler.Instance.IdentificationTap(firstHalfTime, secondHalfTime, v);
    }
}
