using UnityEngine;
using System.Collections;

public class look : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    void Update()
    {
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(-v, h, 0);
    }
}