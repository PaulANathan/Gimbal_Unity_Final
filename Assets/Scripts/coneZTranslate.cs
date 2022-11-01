using UnityEngine;

public class coneZTranslate : MonoBehaviour
{
    //Public variables
    public Material mat0, mat1;

    //Private variables
    private Camera myCam;
    private Transform parentTransform;
    private float startZPos;
    private float startMousePosX;
    private bool isDragging;
    private bool screenLeft;

    private void Start()
    {
        parentTransform = transform.parent.parent.transform;
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

        if (mousePos.x < (Screen.width / 2))
        {
            screenLeft = true;
        }
        else
        {
            screenLeft = false;
        }

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startZPos = parentTransform.localPosition.z;
        startMousePosX = mousePos.x;

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

        if (screenLeft)
        {
            parentTransform.localPosition = new Vector3(parentTransform.localPosition.x, parentTransform.localPosition.y, startZPos + (mousePos.x - startMousePosX)); //(mousePos.x - startZPos)
        }
        else
        {
            //when mouse is right screen, move right translates to negative z
            parentTransform.localPosition = new Vector3(parentTransform.localPosition.x, parentTransform.localPosition.y, (startZPos - (mousePos.x-startMousePosX)));
        }
    }
}