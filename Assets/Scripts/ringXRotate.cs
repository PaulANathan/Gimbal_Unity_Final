using UnityEngine;

public class ringXRotate : MonoBehaviour
{
    private void OnMouseDrag()
    {
        //While user holding down mouse, rotate around x-axis by the specified rate
        float rotSpeed = 80;
        float rotX = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        transform.parent.parent.GetChild(0).Rotate(Vector3.right, rotX, Space.World);
    }
}