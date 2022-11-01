using UnityEngine;

public class ringZRightRotate : MonoBehaviour
{
    private void OnMouseDrag()
    {
        float rotSpeed = 80;
        float rotZ = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;
        transform.parent.parent.GetChild(0).Rotate(Vector3.forward, rotZ, Space.World);
    }
}
