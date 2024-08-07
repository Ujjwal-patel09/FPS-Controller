using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Mouse_Look : MonoBehaviour
{
    [SerializeField]private bool CursorIsLock = false;
    [SerializeField]private float MouseSensitivity;
    [SerializeField]private float Min_Y_Rotation;
    [SerializeField]private float Max_Y_Rotation;
    
    private Transform PlayerTransform;
    private Transform Camera_Holder;
    private float Xrotation;

    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        Camera_Holder = GameObject.FindGameObjectWithTag("camera_holder").GetComponent<Transform>();
        transform.SetParent(Camera_Holder);
        transform.position = Camera_Holder.position;
        if(CursorIsLock){Cursor.lockState = CursorLockMode.Locked;}// for hiding the cursor;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * MouseSensitivity * Time.deltaTime;

        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation,Min_Y_Rotation,Max_Y_Rotation);

        transform.localRotation = Quaternion.Euler(Xrotation,0,0);//for rotating camera only in x axis when dragging mouse in y axis
        PlayerTransform.Rotate(Vector3.up * mouseX);// for rotating player body when dragging mouse in x axis
        
    }
}
