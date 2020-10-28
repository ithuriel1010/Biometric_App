using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        string result = MainControler.Instance.CountPrecentage();
        text.text = result;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
