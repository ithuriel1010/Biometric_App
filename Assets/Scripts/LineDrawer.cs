using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{

    private LineRenderer line;
    //private Vector2 mousePosition;
    private Vector3 mousePosition;
    [SerializeField] private bool simplifyLine = false;
    [SerializeField] private float simplifyTolerance = 0.02f;
    float[] x;
    //float[] y;
    //float[] z;
    private ControlerScript _controlerScript;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        _controlerScript = FindObjectOfType<ControlerScript>();
        //line.useWorldSpace = true;
    }
    private void Update()
    {
        if (Input.GetMouseButton(0)) //Or use GetKey with key defined with mouse button
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Debug.Log(mousePosition);
            mousePosition.z = 1.0f;
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, mousePosition);
            
        }
        if (Input.GetMouseButtonUp(0))
        {
            _controlerScript.NewLine();

            if (simplifyLine)
            {
                line.Simplify(simplifyTolerance);
            }
            enabled = false; //Making this script stop
        }

    }
}
