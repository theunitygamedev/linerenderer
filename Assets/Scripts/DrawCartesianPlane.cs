using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCartesianPlane : MonoBehaviour
{
    private LineRenderer xAxisLineRenderer;
    private LineRenderer yAxisLineRenderer;
    private Vector3 center;
    private Vector3 xAxisStart;
    private Vector3 xAxisEnd;
    private Vector3 yAxisStart;
    private Vector3 yAxisEnd;
    private float screenWidth;
    private float screenHeight;
    private float axisWidth;

    [SerializeField]
    private Color colorXAxis;

    [SerializeField]
    private Color colorYAxis;
    // Start is called before the first frame update
    void Start()
    {
        SetCenter();

        xAxisStart = Camera.main.ScreenToWorldPoint(new Vector3(0f, center.y));
        xAxisStart.z = 0f;
        xAxisEnd = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, center.y));
        xAxisEnd.z = 0f;  
        
        xAxisLineRenderer = gameObject.AddComponent<LineRenderer>();
        
        xAxisLineRenderer.startWidth = xAxisLineRenderer.endWidth = axisWidth;
        xAxisLineRenderer.positionCount = 2;

        SetColor();
        SetStartEndPoints();

    }
    private void SetCenter()
    {
        //get lengths of screen
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        axisWidth = 0.01f;
        center = new Vector3(screenWidth / 2, screenHeight / 2, 0f);
    }
    private void SetColor()
    {
        xAxisLineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        xAxisLineRenderer.material.color = colorXAxis;

    }

    private void SetStartEndPoints()
    {
        xAxisLineRenderer.SetPosition(0, xAxisStart);
        xAxisLineRenderer.SetPosition(1, xAxisEnd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
