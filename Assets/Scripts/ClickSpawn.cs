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
        renderer.material.color = Color.grey;
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }

    private void OnMouseDown()
    {
        newCube = Instantiate(prefab, transform.position, Quaternion.identity);

        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);

        startXPos = mousePos.x - newCube.transform.localPosition.x;
        startYPos = mousePos.y - newCube.transform.localPosition.y;

        isDragging = true;
    }

    private void OnMouseUp()
    {
        isDragging = false;
        newCube = null;
    }

    public void DragObject()
    {
        Vector3 mousePos = Input.mousePosition;

        if (!myCam.orthographic)
        {
            mousePos.z = 10;
        }

        mousePos = myCam.ScreenToWorldPoint(mousePos);
        newCube.transform.localPosition = new Vector3(mousePos.x - startXPos, mousePos.y - startYPos, newCube.transform.localPosition.z);
    }
}