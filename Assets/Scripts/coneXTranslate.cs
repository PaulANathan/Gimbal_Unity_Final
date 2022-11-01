using UnityEngine;

public class coneXTranslate : MonoBehaviour
{
    //Public variables
    public Material mat0, mat1;

    //Private variables
    private Camera myCam;
    private Transform parentTransform;
    private float startXPos;
    private bool isDragging;

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

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - parentTransform.localPosition.x;

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
        parentTransform.localPosition = new Vector3(mousePos.x - startXPos, parentTransform.localPosition.y, parentTransform.localPosition.z);
    }
}