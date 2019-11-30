using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    Vector2 mMove;
    Rigidbody rb;

    // Start is called before the first frame update

    void Start()
    {
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
        Vector3 movement = new Vector3(mMove.x, 0.0f,mMove.y);
        movement.Normalize();
        CharacterController controller = GetComponent<CharacterController>();
        controller.Move(walkSpeed*movement*Time.deltaTime);
    }

}
