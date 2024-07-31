using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        //get center of screen and set vector
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        axisWidth = 0.01f;
        center = new Vector3(screenWidth / 2, screenHeight / 2, 0f);

        //we need to create empty gameobjects for the x and y axis.  
        //only one linelender can be added to a gameobject
        //since lines are drawn by connecting points we will need two so they don't overlap
        //have the ability to control different colors for both

        GameObject xAxisObject = new GameObject("xAxisObject");
        GameObject yAxisObject = new GameObject("yAxisObject");

        //nexst both objects under the current transform
        xAxisObject.transform.parent = transform;
        yAxisObject.transform.parent = transform;

        xAxisLineRenderer = xAxisObject.AddComponent<LineRenderer>();

        xAxisStart = Camera.main.ScreenToWorldPoint(new Vector3(0f, center.y));
        xAxisStart.z = 0f;
        xAxisEnd = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, center.y));
        xAxisEnd.z = 0f;

        xAxisLineRenderer.startWidth = xAxisLineRenderer.endWidth = axisWidth;
        xAxisLineRenderer.positionCount = 2;

        xAxisLineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        xAxisLineRenderer.material.color = colorXAxis;

        //Set start and end points for line renderer.  A line will be automatically drawn in between.
        xAxisLineRenderer.SetPosition(0, xAxisStart);
        xAxisLineRenderer.SetPosition(1, xAxisEnd);

        yAxisLineRenderer = yAxisObject.AddComponent<LineRenderer>();

        yAxisStart = Camera.main.ScreenToWorldPoint(new Vector3(center.x, 0f));
        yAxisStart.z = 0f;
        yAxisEnd = Camera.main.ScreenToWorldPoint(new Vector3(center.x, screenHeight));
        yAxisEnd.z = 0f;

        yAxisLineRenderer.startWidth = yAxisLineRenderer.endWidth = axisWidth;
        yAxisLineRenderer.positionCount = 2;

        yAxisLineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        yAxisLineRenderer.material.color = colorYAxis;

        //Set start and end points for line renderer.  A line will be automatically drawn in between.
        yAxisLineRenderer.SetPosition(0, yAxisStart);
        yAxisLineRenderer.SetPosition(1, yAxisEnd);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
