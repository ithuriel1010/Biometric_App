using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   public void AddPerson()
    {
        Debug.Log("Change scene");
        SceneManager.LoadScene("AddPerson", LoadSceneMode.Additive);

        //SceneManager.LoadScene("AddPerson");
    }
}
