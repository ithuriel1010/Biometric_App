using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class AddPersonScene : MonoBehaviour
{
    public void MoveToLevel1()
    {
        Debug.Log("Change scene");
        SceneManager.LoadScene("NewScene", LoadSceneMode.Single);
    }
}
