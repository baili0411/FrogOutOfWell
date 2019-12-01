using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public UnityEngine.Transform player;
    public Vector3 front;
    public float distanceFromTarget = 4.0f;
    public float mouseSensitivity = 0.5f;
    Vector2 mLook;
    Vector3 FocusPoint;
    float yaw=0.0f;
    float pitch=0.0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 TargetRotation = new Vector3(pitch, yaw);
        transform.eulerAngles=TargetRotation;
        //Quaternion rotation = Quaternion.Euler (0, mLook.x, mLook.y);
        //offset = rotation*offset;
        transform.position = player.position - transform.forward*distanceFromTarget;
        //transform.LookAt(player.position);
    }

    public void OnLook(InputAction.CallbackContext context){
        mLook = context.ReadValue<Vector2>();
        pitch = Mathf.Clamp(pitch - mLook.y*mouseSensitivity,-90,90);
        yaw = yaw + mLook.x*mouseSensitivity;
        while(yaw>360.0){
            yaw -=360.0f;
        }
        while(yaw < -360.0){
            yaw += 360.0f;
        }
    }
}
