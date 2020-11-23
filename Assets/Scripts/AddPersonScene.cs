using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class AddPersonScene : MonoBehaviour
{
    private InputField _field;

    private void Start()
    { 
        //TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
        _field = FindObjectOfType<InputField>();
    }
    public void MoveToLevel1()
    {
        _field = FindObjectOfType<InputField>();
        string name = _field.text;
        MainControler.Instance.GetName(name);
        Debug.Log("Change scene");
        SceneManager.LoadScene("Square", LoadSceneMode.Single);

    }
}
