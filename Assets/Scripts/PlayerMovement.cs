using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public UnityEngine.Transform camera,cameraTarget;
    public float walkSpeed = 1.0f;
    public float weight = 0.01f;
    public float terminalVelocity = 20.0f; 
    public float charge = 0.0f;
    public float chargespeed =0.67f;
    public float jumpSpeed = 3.0f;
    Vector2 mMove;
    Rigidbody rb;
    Vector3 curVelocity;

    bool pressed = false;
    bool charging =false;
    bool jumped = false;
    bool onWall = false;
    // Start is called before the first frame update

    void Start()
    {
         Cursor.lockState = CursorLockMode.Locked;   // keep confined to center of screen
        pressed = false;
        curVelocity = Vector3.zero;
        charge = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Charge();
        Move();
    }

    public void OnMove(InputAction.CallbackContext context){
        mMove = context.ReadValue<Vector2>();
        //Debug.Log(context.ReadValue<Vector2>().ToString());
        //Debug.Log(mMove.ToString());
       // Debug.Log("WTF?");
    }

    public void OnFire(InputAction.CallbackContext context){
        if(context.ReadValue<float>() >0.7){
            pressed = true;
        }
        else {
            pressed = false;
        }
        //Debug.Log(context.ReadValue<float>().ToString());
        //Debug.Log(mMove.ToString());
       // Debug.Log("WTF?");
    }

    void Move(){
        Vector3 front = cameraTarget.position- camera.position;
        front.y = 0;
        front.Normalize();
        Vector3 right = Quaternion.Euler(0, 90, 0)*front;
        Vector3 movement = front*mMove.y+right*mMove.x;
        movement.Normalize();
        CharacterController controller = GetComponent<CharacterController>();
        if(!controller.isGrounded&&!onWall){
            curVelocity.y =curVelocity.y - weight*(9.8f)*Time.deltaTime;
            //Debug.Log(curVelocity.y);
            movement = Vector3.zero;
        }
        else {
            //Debug.Log("Ground!");
            if(!jumped)
            curVelocity = Vector3.zero;
            else {
                jumped = false;
            }
        }
        if(charging||onWall){
            movement = Vector3.zero;
        }
        Vector3  finalmove = walkSpeed*movement*Time.deltaTime+curVelocity*Time.deltaTime;
        controller.Move(finalmove);
        if(finalmove!=Vector3.zero){
            finalmove.y = 0;
            transform.LookAt(transform.position+finalmove);
        }
    }
    void Charge(){
        CharacterController controller = GetComponent<CharacterController>();
       //Debug.Log(pressed);
        if(pressed){
            if(controller.isGrounded||onWall)
                charging = true;
        }
        else {
            //Debug.Log(controller.isGrounded);
            if(charging){
                //Debug.Log("JUMP");
                Jump();
            }
            charging = false;
        }
        if(charging){
            charge = Mathf.Clamp(charge+chargespeed*Time.deltaTime,0,1);
        }  
        else {
            charge = 0.0f;
        }
    }
    void Jump(){
        Vector3 front = cameraTarget.position- camera.position;
        front.Normalize();
        curVelocity = front*charge*jumpSpeed;
        jumped =true;
        onWall = false;
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.collisionFlags == CollisionFlags.Sides)
        {
            onWall=true;
        }
    }

}
