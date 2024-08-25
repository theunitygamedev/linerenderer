using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class DrawLineWithMouse : MonoBehaviour
{
    private LineRenderer line;
    private Vector3 previousPosition;

    //user SerializeField to make class variables available to the editor without
    //having to make them public.  This way you can maintain encapsulation.
    [SerializeField]
    private float minDistance = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = line.endWidth = 0.1f;
        line.material = new Material(Shader.Find("Unlit/Color"));
        line.material.color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            currentPosition.z = 0f;



            if (Vector3.Distance(currentPosition,previousPosition) > minDistance)
            {
                line.positionCount++;
                line.SetPosition(line.positionCount - 1, currentPosition);
                DrawCircle("Circle" + line.positionCount,Color.blue, currentPosition);

                previousPosition = currentPosition;
            }
        }
    }

    void DrawCircle(string name, UnityEngine.Color color, Vector3 position)
    {
        GameObject gameObject = new GameObject(name);
        float theta_scale = 0.03f;  // Circle resolution
        float r = 0.2f;

        
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = line.endWidth = 0.04f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = color;
        lineRenderer.positionCount = 0;
        //lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
        //lineRenderer.SetColors(c1, c2);
        //lineRenderer.SetWidth(0.2f, 0.2f);
        //lineRenderer.SetVertexCount(size);

        int i = 0;
        for (float theta = 0f; theta < 2.0f * Mathf.PI; theta += theta_scale)
        {
            lineRenderer.positionCount++;
            float x = r * Mathf.Cos(theta);
            float y = r * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x + position.x, y + position.y, 0);
            lineRenderer.SetPosition(i, pos);
            
            i ++;
        }
    }
}

