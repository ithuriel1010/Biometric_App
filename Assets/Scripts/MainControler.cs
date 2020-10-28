using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    private string database = "test4";
    public void AddToDatabase()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + database;
		
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Create table
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, name CHAR , squareP1 CHAR, squareP2 CHAR, squareP3 CHAR, squareP4 CHAR, squareP5 CHAR)";
		
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        // Insert values in table
        
        IDbCommand cmnd = dbcon.CreateCommand();
        
        
        cmnd.CommandText = $"INSERT INTO my_table (name, squareP1, squareP2, squareP3, squareP4, squareP5) VALUES ('{userName}', '{orderOfPointsSquare[0]}', '{orderOfPointsSquare[1]}', '{orderOfPointsSquare[2]}', '{orderOfPointsSquare[3]}', '{orderOfPointsSquare[4]}')";
        cmnd.ExecuteNonQuery();

        
        dbcon.Close();

    }

    public string Check()
    {
        string connection = "URI=file:" + Application.persistentDataPath + "/" + database;
        string nameResulted="No such person";
        // Open connection
        IDbConnection dbcon = new SqliteConnection(connection);
        dbcon.Open();

        // Create table
        IDbCommand dbcmd;
        dbcmd = dbcon.CreateCommand();
        string q_createTable = "CREATE TABLE IF NOT EXISTS my_table (id INTEGER PRIMARY KEY, name CHAR , squareP1 CHAR, squareP2 CHAR, squareP3 CHAR, squareP4 CHAR, squareP5 CHAR)";
		
        dbcmd.CommandText = q_createTable;
        dbcmd.ExecuteReader();

        // Insert values in table
        
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        var query ="SELECT * FROM my_table";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        int correct=0;
        
        while (reader.Read())
        {
            correct = 0;
            
            for (int i = 0; i < orderOfPointsSquareIdentification.Length; i++)
            {
                Debug.Log("R: "+reader[i+2]);
                Debug.Log("Order: "+orderOfPointsSquareIdentification[i]);


                if (orderOfPointsSquareIdentification[i] == reader[i+2].ToString())
                {
                    correct++;
                }
            }

            Debug.Log("corr: "+correct);
            if (correct == orderOfPointsSquareIdentification.Length)
            {
                nameResulted = reader[1].ToString();
            }
            
        }
        
        // Close connection
        dbcon.Close();
        return nameResulted;
    }
}
