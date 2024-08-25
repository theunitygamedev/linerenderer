using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

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

    [SerializeField]
    private float unitFontSize;

    private GameObject canvasObject;


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

        canvasObject = new GameObject("Canvas");
        canvasObject.AddComponent<Canvas>();
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

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

        lr = DrawAxisUnits("yAxisUnitTicks", numXAxisUnits, colorYAxis, center, screenWidth, screenHeight, axisTickWidth, 20.0f, "y");
        lr.transform.parent = transform;

    }

    private void PlaceText(string text, Vector2 position, GameObject canvasObject)
    {
        
        GameObject tmProObject = new GameObject(text);
        TextMeshPro tm = tmProObject.AddComponent<TextMeshPro>();
        tmProObject.transform.parent = canvasObject.transform;
        canvasObject.transform.parent = this.transform;
        tm.text = text;
        tm.fontSize = unitFontSize;
        tm.alignment = TextAlignmentOptions.Center;

        RectTransform rt = tm.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(2f, 0f);

        Vector3 pointScreen = new Vector3(position.x, position.y-10.0f);
        Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
        pointWorld.z = 0f;

        tm.transform.position = pointWorld;

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

        int unitCount = (numUnits * -1);
        //draw axis units 
        if (axis == "x")
        {
            float curX = 0f;
            float curY;

            for (int i = 0; i < numUnitsNeeded; i++)
            {
                //start on x axis at left most point of screen
                SetLinePosition(curX, center.y, line,i);

                i++;

                //go up 1/2 unitTickLineLength length (above x axis)
                curY = center.y + tickerHalf;
                SetLinePosition(curX, curY, line, i);

                i++;

                //go down 1 unitTickLineLength (below x axis)
                curY = center.y - tickerHalf;
                SetLinePosition(curX, curY, line, i);
                PlaceText(unitCount.ToString(), new Vector2(curX, curY),canvasObject);
                unitCount++;

                //this is where we want to draw the unit desgination like 1, 2, 10 etc


                i++;

                //go back to center.y which is the x axis y coordinate
                curY = center.y;
                SetLinePosition(curX, curY, line, i);

                //move to the right by stepLength distance
                curX += stepLength;
                Debug.Log(i);
            }
        }
        else //y axis
        {
            float curX;
            float curY;

            // draw the same number of y unit tickers as x. THis will also handle the case where a screen is longer in the y axis
            curY = center.y - (screenWidth / 2.0f);

            for (int i = 0; i < numUnitsNeeded; i++)
            {
                //start on y axis at bottom of screen
                SetLinePosition(center.x, curY, line, i);

                i++;

                //go left 1/2 unitTickLineLength length (left of y axis)
                curX = center.x - tickerHalf;
                SetLinePosition(curX, curY, line, i);

                i++;

                //go right 1 unitTickLineLength (right of y axis)
                curX = center.x + tickerHalf;
                SetLinePosition(curX, curY, line, i);

                i++;

                //go back to y axis x coordinate
                curX = center.x;
                SetLinePosition(curX, curY, line, i);

                //move to the right by stepLength distance
                curY += stepLength;
                //Debug.Log(i);
            }
        }
            
        return line;
    }

    private void SetLinePosition(float x, float y, LineRenderer line, int index)
    {
        Vector3 pointScreen = new Vector3(x, y);
        Vector3 pointWorld = Camera.main.ScreenToWorldPoint(pointScreen);
        pointWorld.z = 0f;
        line.SetPosition(index, pointWorld);
    }

    private void DrawUnit()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
