using UnityEngine;

public class sphereXScale : MonoBehaviour
{
    //Public variables
    public Material mat0, mat1;

    //Private variables
    private Camera myCam;
    private float startXPos;
    private bool isDragging;
    private Vector3 startScale;

    private void Start()
    {
        isDragging = false;
        myCam = Camera.main;
    }

    private void Update()
    {
        if (isDragging)
        {
            DragObject();
        }
    }

    private void OnMouseEnter()
    {
        transform.GetComponent<Renderer>().material = mat1;
    }

    private void OnMouseExit()
    {
        if (!isDragging)
        {
            transform.GetComponent<Renderer>().material = mat0;
        }
    }

    private void OnMouseDown()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x;
        startScale = transform.parent.parent.GetChild(0).localScale;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        transform.GetComponent<Renderer>().material = mat0;
    }

    public void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        float scaleDelta = -(mousePos.x - startXPos);
        Vector3 scaleDeltaVect;


        // Handle rotation changes
        Vector3 forwardDir = transform.parent.parent.GetChild(0).forward;
        Vector3 upDir = transform.parent.parent.GetChild(0).up;

        if (Mathf.Abs(forwardDir.z) > Mathf.Abs(forwardDir.x) && Mathf.Abs(forwardDir.z) > Mathf.Abs(forwardDir.y))
        {
            //Foward facing forward/back

            if (Mathf.Abs(upDir.x) > Mathf.Abs(upDir.y))
            {
                //Up facing left/right -> scale y
                scaleDeltaVect = Vector3.up * scaleDelta;
            }
            else
            {
                //Up facing up/down -> scale x
                scaleDeltaVect = Vector3.right * scaleDelta;
            }
        }
        else if (Mathf.Abs(forwardDir.x) > Mathf.Abs(forwardDir.y) && Mathf.Abs(forwardDir.x) > Mathf.Abs(forwardDir.z))
        {
            //Forward facing left/right

            scaleDeltaVect = Vector3.forward * scaleDelta;
        }
        else
        {
            //Foward facing up/down

            if (Mathf.Abs(upDir.x) > Mathf.Abs(upDir.y))
            {
                //Up facing left/right -> scale y
                scaleDeltaVect = Vector3.up * scaleDelta;
            }
            else
            {
                //Up facing forward/back -> scale x
                scaleDeltaVect = Vector3.right * scaleDelta;
            }
        }


        // Handle negative scaling
        Vector3 newScale = startScale + scaleDeltaVect;

        if (newScale.x < 0.02f)
        {
            newScale.x = 0.02f;
        }
        else if (newScale.y < 0.02f)
        {
            newScale.y = 0.02f;
        }
        else if (newScale.z < 0.02f)
        {
            newScale.z = 0.02f;
        }

        transform.parent.parent.GetChild(0).localScale = newScale;
    }
}