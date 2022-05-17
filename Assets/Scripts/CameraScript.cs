using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject Player = null;

    float Rotationspeed = 0.1f;

    float speed = 8.0f;

    float yaw = 0.0f;
    float pitch = 0.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {

        transform.position = Player.transform.position;


        yaw += speed * Input.GetAxis("Mouse X");
        if (pitch < -90 - speed)
        {
            pitch = -90;
        }
        else if (pitch > 90 + speed)
        {
            pitch = 90;
        }
        else
        {
            pitch -= speed * Input.GetAxis("Mouse Y");
        }
        transform.localEulerAngles = new Vector3(pitch, yaw, 0.0f);
        
    }

    public void SetRotation(Vector3 rot)
    {
        pitch = rot.x;
        yaw = rot.y;
    }
}
