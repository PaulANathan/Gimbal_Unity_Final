using UnityEngine;

public class ClickSpawn : MonoBehaviour
{
    // Public variables
    public GameObject prefab;

    // Private variables
    private Camera myCam;
    private Renderer renderer;
    private float startXPos, startYPos;
    private bool isDragging = false;
    private GameObject newCube;

    // Start is called before the first frame update
    void Start()
    {
        myCam = Camera.main;
        renderer = GetComponent<Renderer>();
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
        //While mouse hovering, switch object color to grey
        renderer.material.color = Color.grey;
    }

    private void OnMouseExit()
    {
        //While mouse not hovering, switch object color to white
        renderer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        //On click instantiate new gimbal prefab at the same position as the cube button
        newCube = Instantiate(prefab, transform.position, Quaternion.identity);

        //Convert mouse position from screen to world space
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        //Store initial offset of mouse position and newly created gimbal position
        startXPos = mousePos.x - newCube.transform.localPosition.x;
        startYPos = mousePos.y - newCube.transform.localPosition.y;

        //Let system know dragging is initiated
        isDragging = true;
    }

    private void OnMouseUp()
    {
        //When no longer holding down mouse button reset isDragging and newCube reference 
        isDragging = false;
        newCube = null;
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

        //Set newCube position based on the difference between the current mouse position and the initial mouse position when drag started
        newCube.transform.localPosition = new Vector3(mousePos.x - startXPos, mousePos.y - startYPos, newCube.transform.localPosition.z);
    }
}