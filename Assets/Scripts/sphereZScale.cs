using UnityEngine;

public class sphereZScale : MonoBehaviour
{
    //Public variables
    public Material mat0, mat1;

    //Private variables
    private Camera myCam;
    private float startZPos, startZPos2;
    private bool isDragging;
    private int screenQuad;
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

        // Find which quadrant mouse is on screen
        // 0 = bottom-left quad, 1 = bottom-right quad, 2 = top-left quad, 3 = top-right quad
        if (mousePos.x < (Screen.width / 2) && mousePos.y < (Screen.height / 2))
        {
            screenQuad = 0;
        } 
        else if (mousePos.x > (Screen.width / 2) && mousePos.y < (Screen.height / 2))
        {
            screenQuad = 1;
        }
        else if (mousePos.x < (Screen.width / 2) && mousePos.y > (Screen.height / 2))
        {
            screenQuad = 2;
        } 
        else
        {
            screenQuad = 3;
        }

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startZPos = mousePos.x;
        startZPos2 = mousePos.y;
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

        float scaleDelta;


        // Handle quadrant affect on scaling
        // 0 = bottom-left quad, 1 = bottom-right quad, 2 = top-left quad, 3 = top-right quad
        if (screenQuad==0)
        {
            //mouse moving negative x and negative y increases scale
            scaleDelta = (-(mousePos.x - startZPos) - (mousePos.y - startZPos2)) / 2;
        }
        else if (screenQuad==1)
        {
            //mouse moving positive x and negative y increases scale
            scaleDelta = ((mousePos.x - startZPos) - (mousePos.y - startZPos2)) / 2;
        }
        else if (screenQuad==2)
        {
            //mouse moving negative x and positive y increases scale
            scaleDelta = (-(mousePos.x - startZPos) + (mousePos.y - startZPos2)) / 2;
        }
        else
        {
            //mouse moving positive x and positive y increases scale
            scaleDelta = ((mousePos.x - startZPos) + (mousePos.y - startZPos2)) / 2;
        }

        Vector3 scaleDeltaVect;


        // Handle rotation changes
        Vector3 forwardDir= transform.parent.parent.GetChild(0).forward;
        Vector3 upDir = transform.parent.parent.GetChild(0).up;

        if (Mathf.Abs(forwardDir.z) > Mathf.Abs(forwardDir.x) && Mathf.Abs(forwardDir.z) > Mathf.Abs(forwardDir.y)) {
            
            //Forward facing forward/back

            scaleDeltaVect = Vector3.forward * scaleDelta;
        }  
        else if (Mathf.Abs(forwardDir.x) > Mathf.Abs(forwardDir.y) && Mathf.Abs(forwardDir.x) > Mathf.Abs(forwardDir.z))
        {
            //Forward facing left/right

            if (Mathf.Abs(upDir.z) > Mathf.Abs(upDir.y))
            {
                //Up facing forward/back -> scale y
                scaleDeltaVect = Vector3.up * scaleDelta;
            }
            else
            {
                //Up facing up/down -> scale x
                scaleDeltaVect = Vector3.right * scaleDelta;
            }
        }
        else
        {
            //Forward facing up/down

            if (Mathf.Abs(upDir.z) > Mathf.Abs(upDir.x))
            {
                //Up facing forward/back -> scale y
                scaleDeltaVect = Vector3.up * scaleDelta;
            }
            else
            {
                //Up facing left/right -> scale x
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