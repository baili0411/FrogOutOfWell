using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public UnityEngine.Transform camera;
    public float walkSpeed = 1.0f;
    public float weight = 0.01f;
    public float terminalVelocity = 20.0f; 
    Vector2 mMove;
    Rigidbody rb;

    Vector3 curVelocity;

    // Start is called before the first frame update

    void Start()
    {
        curVelocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void OnMove(InputAction.CallbackContext context){
        mMove = context.ReadValue<Vector2>();
        //Debug.Log(context.ReadValue<Vector2>().ToString());
        //Debug.Log(mMove.ToString());
       // Debug.Log("WTF?");
    }

    void Move(){
        Vector3 front = transform.position- camera.position;
        front.y = 0;
        front.Normalize();
        Vector3 right = Quaternion.Euler(0, 90, 0)*front;
        Vector3 movement = front*mMove.y+right*mMove.x;
        movement.Normalize();
        CharacterController controller = GetComponent<CharacterController>();
        if(!controller.isGrounded){
            curVelocity.y = Mathf.Clamp(curVelocity.y - weight*(0.98f)*Time.deltaTime,-terminalVelocity,terminalVelocity);
            //Debug.Log(curVelocity.y);
            movement = Vector3.zero;
        }
        else {
            //Debug.Log("Ground!");
            curVelocity.y = 0;
        }
        controller.Move(walkSpeed*movement*Time.deltaTime+curVelocity*Time.deltaTime);
        if(movement!=Vector3.zero){
            transform.LookAt(transform.position+movement);
        }
    }

}
