using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrawCartesianPlane : MonoBehaviour
{
    [SerializeField]
    private int numXAxisUnits;

    [SerializeField]
    private string xAxisLabel;

    [SerializeField]
    private string yAxisLabel;

    [SerializeField]
    private float axisWidth;

    [SerializeField]
    private Color colorXAxis;

    [SerializeField]
    private Color colorYAxis;
    // Start is called before the first frame update
    void Start()
    {
        //get center of screen and set vector

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float axisWidth = 0.01f;
        Vector3 center = new Vector3(screenWidth / 2, screenHeight / 2, 0f);

        Vector2 startPoint = new Vector2(0f, center.y);
        Vector2 endPoint = new Vector2(screenWidth, center.y);
        LineRenderer lr;

        //draw x axis
        lr = DrawLine("xAxisObject", startPoint, endPoint, colorXAxis, axisWidth);
        lr.transform.parent = transform;

        //draw y axis
        startPoint = new Vector2(center.x, 0f);
        endPoint = new Vector2(center.x, screenHeight);
        lr = DrawLine("yAxisObject", startPoint, endPoint, colorYAxis, axisWidth);
        lr.transform.parent = transform;

        //add


    }

    private LineRenderer DrawLine(string name, Vector2 startPoint, Vector2 endPoint, Color color, float lineWidth)
    {
        GameObject gameObject = new GameObject(name);

        LineRenderer line = gameObject.AddComponent<LineRenderer>();

        Vector3 startWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(startPoint.x, startPoint.y));
        startWorldPoint.z = 0f;
        Vector3 endWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(endPoint.x, endPoint.y));
        endWorldPoint.z = 0f;

        line.startWidth = line.endWidth = lineWidth;
        line.positionCount = 2;

        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = color;

        //Set start and end points for line renderer.  A line will be automatically drawn in between.
        line.SetPosition(0, startWorldPoint);
        line.SetPosition(1, endWorldPoint);

        return line;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
