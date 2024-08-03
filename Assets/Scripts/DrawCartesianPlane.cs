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

    [SerializeField]
    private float axisTickWidth;


    // Start is called before the first frame update
    void Start()
    {
        //get center of screen and set vector

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
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

        //draw unit ticks on x and y axis using only two line renderers each having multiple points
        
        lr = DrawAxisUnits("xAxisUnitTicks", numXAxisUnits, colorXAxis, center, screenWidth, screenHeight,axisTickWidth,20.0f,"x");
        lr.transform.parent = transform;

        lr = DrawAxisUnits("yAxisUnitTicks", numXAxisUnits, colorXAxis, center, screenWidth, screenHeight, axisTickWidth, 20.0f, "y");
        lr.transform.parent = transform;

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

    private LineRenderer DrawAxisUnits(string name, int numUnits, Color color, Vector3 center, float screenWidth, float screenHeight, float lineWidth, float unitTickLineLength, string axis)
    {
        GameObject gameObject = new GameObject(name);

        LineRenderer line = gameObject.AddComponent<LineRenderer>();

        //Vector3 startWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, center.y));
        //startWorldPoint.z = 0f;
        //Vector3 endWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, center.y));
        //endWorldPoint.z = 0f;

        int numUnitsNeeded = (numUnits + (3 * numUnits) + 4) * 2;
        line.startWidth = line.endWidth = lineWidth;
        line.positionCount = numUnitsNeeded;

        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = color;

        

        //line.SetPosition(0, startWorldPoint);
        float stepLength = screenWidth / 2.0f / numUnits;
        float tickerHalf = unitTickLineLength / 2.0f;
        //draw axis units 
        if (axis == "x")
        {
            float curX = 0f;
            float curY = center.y;

            for (int i = 0; i < numUnitsNeeded; i++)
            {
                //start on x axis at left most point of screen
                Vector3 pointScreen = new Vector3(curX, center.y);
                Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                i++;

                //go up 1/2 unitTickLineLength length (above x axis)
                curY = center.y + tickerHalf;
                pointScreen = new Vector3(curX, curY);
                pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                i++;

                //go down 1 unitTickLineLength (below x axis)
                curY = center.y - tickerHalf;
                pointScreen = new Vector3(curX, curY);
                pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                i++;
                //go back to x axis which center.y (back to x axis) 
                curY = center.y;
                pointScreen = new Vector3(curX, curY);
                pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                //move to the right by stepLength distance
                curX += stepLength;
                Debug.Log(i);
            }
        }
        else //y axis
        {
            float curX = center.x;
            float curY = 0f;

            for (int i = 0; i < numUnitsNeeded; i++)
            {
                //start on y axis at bottom of screen
                Vector3 pointScreen = new Vector3(center.x, 0f);
                Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                i++;

                //go left 1/2 unitTickLineLength length (left of y axis)
                curX = center.x - tickerHalf;
                pointScreen = new Vector3(curX, curY);
                pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                i++;

                //go right 1 unitTickLineLength (right of y axis)
                curX = center.x + tickerHalf;
                pointScreen = new Vector3(curX, curY);
                pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                i++;
                //go back to y axis x coordinate
                curX = center.x;
                pointScreen = new Vector3(curX, curY);
                pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
                pointWorld.z = 0f;
                line.SetPosition(i, pointWorld);

                //move to the right by stepLength distance
                curY += stepLength;
                Debug.Log(i);
            }
        }
            
        



        return line;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
