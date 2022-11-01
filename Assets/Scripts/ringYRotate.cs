using UnityEngine;

public class ringYRotate : MonoBehaviour
{
    private void OnMouseDrag()
    {
        //While user holding down mouse, rotate around y-axis by the specified rate
        float rotSpeed = 80;
        float rotY = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        transform.parent.parent.GetChild(0).Rotate(Vector3.up, -rotY, Space.World);
    }
}
