using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Player_Movement : MonoBehaviour
{
    
   [Header("Walking & Running")]
   private CharacterController characterController;
   private Vector3 MoveDirection;
   private Vector3 velocity;
   private float MoveSpeed;
   
   public float walking_speed;
   public float running_speed;
   public bool Walking;
   public bool Running;
    
   [Header("Jump & Ground_Check")]
   public float gravity = -10f;
   public Transform GroundCheck;
   public float GroundCheck_Sphere_radius = 0.4f;
   public LayerMask GroundMask;
   public bool isGrounded;
   public float JumpHight;
   public bool Jump;
    
   
   void Start()
   {
      characterController = GetComponent<CharacterController>();
   }

   void Update()
   {
      movement();
      Jump_GroundCheck();
   }

   public void movement()
   {
      float x = Input.GetAxis("Horizontal");
      float z = Input.GetAxis("Vertical");
        
      // for move in direction of  camera facing//
      MoveDirection = transform.right * x + transform.forward * z;
      characterController.Move(MoveDirection * MoveSpeed * Time.deltaTime);
      
      // for walking //
      if(MoveDirection.magnitude > 0.1f)
      {
         Walking = true;
         MoveSpeed = walking_speed;
      }else{
         Walking = false;
      }

      // for Running //
      if(MoveDirection.magnitude > 0.1f && Input.GetKey(KeyCode.LeftShift))
      {
         Running = true;
         MoveSpeed = running_speed;
      }else{
         Running = false;
      }

   }

   void Jump_GroundCheck()
   {
      // for fall in ground using gravity and velocity //
      velocity.y += gravity * Time.deltaTime;
      characterController.Move(velocity * Time.deltaTime);

      // for reset the velocity when player on ground //
      isGrounded = Physics.CheckSphere(GroundCheck.position,GroundCheck_Sphere_radius,GroundMask);
      if(isGrounded && velocity.y < 0)
      {
         velocity.y = -2f;
      }

      // for Jump //
      if(Input.GetButtonDown("Jump") && isGrounded)
      {
         velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);
         Jump = true;
      }else{
         Jump = false;
      }
   }

}
