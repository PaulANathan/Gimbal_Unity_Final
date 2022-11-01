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
        //While mouse hovering, switch material to highlighted
        transform.GetComponent<Renderer>().material = mat1;
    }

    private void OnMouseExit()
    {
        //While mouse not hovering or not dragging, switch material to original
        if (!isDragging)
        {
            transform.GetComponent<Renderer>().material = mat0;
        }
    }

    private void OnMouseDown()
    {
        //Convert mouse position from screen to world space
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        //Store initial offset of mouse position and current gimbal x position
        startXPos = mousePos.x;
        startScale = transform.parent.parent.GetChild(0).localScale;

        //Let system know dragging is initiated
        isDragging = true;
    }

    private void OnMouseUp()
    {
        //When no longer holding down mouse button reset isDragging and material
        isDragging = false;
        transform.GetComponent<Renderer>().material = mat0;
    }

    public void DragObject()
    {
        //Convert mouse position from screen to world space
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        //Specify scaling difference based on mouse position
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

        //Scale gimbal
        transform.parent.parent.GetChild(0).localScale = newScale;
    }
}