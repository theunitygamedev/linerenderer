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

                previousPosition = currentPosition;
            }
        }
    }
}
