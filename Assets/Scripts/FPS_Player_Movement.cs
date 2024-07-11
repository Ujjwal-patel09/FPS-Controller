using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player_Movement : MonoBehaviour
{
    private CharacterController characterController;
    
    [Header("Movement")]
    public float MoveSpeed = 10f;
    private Vector3 velocity;
    
    [Header("Ground Check")]
    public float gravity = -10f;
    public Transform GroundCheck;
    public float GroundCheck_Sphere_radius = 0.4f;
    public LayerMask GroundMask;
    public bool isGrounded;

    [Header("Jump")]
    public float JumpHight;

   
    void Start()
    {
       characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
       movement();
       Ground_Check();
       Jump();
    }

    void movement()
    {
       float x = Input.GetAxis("Horizontal");
       float z = Input.GetAxis("Vertical");
        
       // for move in direction of  camera facing//
       Vector3 MoveDirection = transform.right * x + transform.forward * z;
       characterController.Move(MoveDirection * MoveSpeed * Time.deltaTime);

       // for fall in ground using gravity and velocity //
       velocity.y += gravity * Time.deltaTime;
       characterController.Move(velocity * Time.deltaTime);
    }

    void Ground_Check()
    {
       // for reset the velocity when player on ground //
       isGrounded = Physics.CheckSphere(GroundCheck.position,GroundCheck_Sphere_radius,GroundMask);

       if(isGrounded && velocity.y < 0)
       {
          velocity.y = -2f;
       }
    }

    void Jump()
    {
       if(Input.GetButtonDown("Jump") && isGrounded)
       {
          velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);
       }
    }
}
