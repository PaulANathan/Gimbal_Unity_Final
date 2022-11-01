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
        //Store grandparent's transform for easier reference
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
        startXPos = mousePos.x - parentTransform.localPosition.x;

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

        //Set gimbal position based on the difference between the current mouse position and the initial mouse position when drag started
        parentTransform.localPosition = new Vector3(mousePos.x - startXPos, parentTransform.localPosition.y, parentTransform.localPosition.z);
    }
}