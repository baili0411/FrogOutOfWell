using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public UnityEngine.Transform player;
    Vector3 offset;
    Vector2 mLook;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler (mLook.y, mLook.x, 0);
        offset = rotation*offset;
        transform.position = player.position + offset;
        transform.LookAt(player.position);
    }

    public void OnLook(InputAction.CallbackContext context){
        mLook = context.ReadValue<Vector2>();

    }
}
