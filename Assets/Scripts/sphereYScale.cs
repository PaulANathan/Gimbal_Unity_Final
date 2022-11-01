using UnityEngine;

public class sphereYScale : MonoBehaviour
{
    //Public variables
    public Material mat0, mat1;

    //Private variables
    private Camera myCam;
    private float startYPos;
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

        //Store initial offset of mouse position and current gimbal y position
        startYPos = mousePos.y;
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
        float scaleDelta = (mousePos.y - startYPos);
        Vector3 scaleDeltaVect;


        // Handle rotation changes
        Vector3 forwardDir = transform.parent.parent.GetChild(0).forward;
        Vector3 upDir = transform.parent.parent.GetChild(0).up;

        if (Mathf.Abs(upDir.y) > Mathf.Abs(upDir.x) && Mathf.Abs(upDir.y) > Mathf.Abs(upDir.z))
        {
            //Up facing up/down -> scale y

            scaleDeltaVect = Vector3.up * scaleDelta;
        }
        else if (Mathf.Abs(upDir.z) > Mathf.Abs(upDir.x) && Mathf.Abs(upDir.z) > Mathf.Abs(upDir.y))
        {
            //Up facing foward/back

            if (Mathf.Abs(forwardDir.y) > Mathf.Abs(forwardDir.x))
            {
                //Forward facing up/down -> scale z
                scaleDeltaVect = Vector3.forward * scaleDelta;
            }
            else
            {
                //Forward facing left/right -> scale x
                scaleDeltaVect = Vector3.right * scaleDelta;
            }
        }
        else
        {
            //Up facing left/right

            if (Mathf.Abs(forwardDir.z) > Mathf.Abs(forwardDir.x))
            {
                //Forward facing forward/back -> scale x
                scaleDeltaVect = Vector3.right * scaleDelta;
            }
            else
            {
                //Forward facing forward/back -> scale z
                scaleDeltaVect = Vector3.forward * scaleDelta;
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