using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToLevel3()
    {
        SceneManager.LoadScene("Cross", LoadSceneMode.Single);
    }
    
    public void MoveToIdentifyLevel3()
    {
        SceneManager.LoadScene("IdentifyCross", LoadSceneMode.Single);
    }
}
