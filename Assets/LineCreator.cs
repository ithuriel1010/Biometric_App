using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    [SerializeField] private GameObject line;
    private Vector2 mousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) //Or use GetKeyDown with key defined with mouse button
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(line, Vector3.zero, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        }
    }
}