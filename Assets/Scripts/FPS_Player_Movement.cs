using System.Collections;
using UnityEngine;

public class FPS_Player_Movement : MonoBehaviour
{
   private CharacterController characterController;

   private Vector3 MoveDirection;
   private Vector3 velocity;
   private float MoveSpeed;
   
   // player Input //
   [HideInInspector]public  float InputX;
   [HideInInspector]public  float InputZ;
   private bool Jump_input;
   private bool Crouch_input;

   [Header("Walking")]
   [SerializeField] private bool Can_Walk = true;
   [SerializeField] private float walking_speed = 3f;
   [HideInInspector]public bool iswalking;
   
   [Header("Running")]
   [SerializeField] private bool Can_Run = true;
   [SerializeField] private float running_speed = 6f;
   [HideInInspector]public bool isrunning;
    
   [Header("Ground_Check")]
   [SerializeField] private float gravity = -10f;
   [SerializeField] private Transform GroundCheck_object;
   [SerializeField] private float GroundCheck_Sphere_radius = 0.4f;
   [SerializeField] private LayerMask GroundMask;
   [SerializeField] private bool isGrounded;

   [Header("Jumping")]
   [SerializeField] private  bool canJump = true;
   [SerializeField] private float JumpHight;
   [SerializeField] private float Jump_CoolDown_Time;
   [HideInInspector]public bool isJumping;

   [Header("Crouching")]
   [SerializeField] private bool CanCrouch = true;
   [SerializeField] private float crouch_Speed = 1.5f;
   [SerializeField] private float Stand_hight;
   [SerializeField] private float Crouch_hight;
   [SerializeField] private float Time_to_Crouch = 0.25f;
   [SerializeField] private Vector3 Standing_Center;
   [SerializeField] private Vector3 Crouching_Center;
   [SerializeField] private float CrouchRadius;
   [SerializeField] private float StandRadius;
   [SerializeField] private GameObject camera_holder;
   [SerializeField] private Vector3 Stand_Cam_Pos;
   [SerializeField] private Vector3 Crouch_cam_pos;
   [HideInInspector]public bool isCrouching;
    
   
   void Start()
   {
      characterController = GetComponent<CharacterController>();
      camera_holder = GameObject.FindGameObjectWithTag("camera_holder");
   }

   void Update()
   {
      PlayerInput();
      movement();
      GroundCheck();
      Jump();
      Crouching();
   }

   private void PlayerInput()
   {
      InputX = Input.GetAxis("Horizontal");
      InputZ = Input.GetAxis("Vertical");
      Jump_input = Input.GetButtonDown("Jump");
      Crouch_input = Input.GetKeyDown(KeyCode.LeftControl);
   }

   private void movement()
   {    
      // for move in direction of  camera facing//
      MoveDirection = transform.right * InputX + transform.forward * InputZ;
      characterController.Move(MoveDirection.normalized * MoveSpeed * Time.deltaTime);

      //  for walking //
      if(Can_Walk)
      {
         if(MoveDirection.magnitude > 0.1f || MoveDirection.magnitude < -0.1f)
         {
            MoveSpeed = walking_speed;
            iswalking = true;  
         }else
         {
            iswalking = false;
         }
      }
      
      // for Running //
      if(Can_Run)
      {
         if(MoveDirection.magnitude > 0.1f && Input.GetKey(KeyCode.LeftShift))
         {
            isrunning = true;
            MoveSpeed = running_speed;
         }else
         {
            isrunning = false;
         }
      }
   }

   private void GroundCheck()
   {
      // for fall in ground using gravity and velocity //
      velocity.y += gravity * Time.deltaTime;
      characterController.Move(velocity * Time.deltaTime);

      // for reset the velocity when player on ground //
      isGrounded = Physics.CheckSphere(GroundCheck_object.position,GroundCheck_Sphere_radius,GroundMask);
      if(isGrounded && velocity.y < 0)
      {
         velocity.y = -2f;
      }
   }

   private void Jump()
   {
      if(canJump)
      {
         if(Jump_input && isGrounded)
         {
            isJumping = true;
            velocity.y = Mathf.Sqrt(JumpHight * -2f * gravity);// Physics Formula // 
            StartCoroutine(jumping_CoolDown());
         }
   
         IEnumerator jumping_CoolDown()
         {
            yield return new WaitForSeconds(Jump_CoolDown_Time);
            isJumping = false;
         }
      }
   }
   
   private void Crouching()
   {
      if(CanCrouch)
      {
         if(Crouch_input && isGrounded)
         {
            isCrouching = !isCrouching;
            if(isCrouching)
            {
               MoveSpeed = crouch_Speed;
               Can_Walk = false;
               Can_Run = false;
               canJump = false;
            }else
            {
               Can_Walk = true;
               Can_Run = true;
               canJump = true;
            }
            StartCoroutine(Crouch_Stand());
         }
      }
   }

   IEnumerator Crouch_Stand()
   {
      float timeTaken = 0;
      float TargetHight = isCrouching? Crouch_hight : Stand_hight;
      float CurrentHight = characterController.height;

      Vector3 TargetCenter = isCrouching? Crouching_Center : Standing_Center;
      Vector3 CurrentCenter = characterController.center;

      float TargetRadius = isCrouching? CrouchRadius : StandRadius;
      float CurrentRadius = characterController.radius;

      Vector3 Target_Cam_Pos = isCrouching? Crouch_cam_pos : Stand_Cam_Pos;
      Vector3 Current_Cam_Pos = camera_holder.transform.localPosition;

      while(timeTaken < Time_to_Crouch)
      {
         characterController.height = Mathf.Lerp(CurrentHight,TargetHight,timeTaken/Time_to_Crouch);
         characterController.center = Vector3.Lerp(CurrentCenter,TargetCenter,timeTaken/Time_to_Crouch);
         characterController.radius = Mathf.Lerp(CurrentRadius,TargetRadius,timeTaken/Time_to_Crouch);
         camera_holder.transform.localPosition = Vector3.Lerp(Current_Cam_Pos,Target_Cam_Pos,timeTaken/Time_to_Crouch);
         timeTaken += Time.deltaTime;
         yield return null;
      }

      characterController.height = TargetHight;
      characterController.center = TargetCenter;
      characterController.radius = TargetRadius;
      camera_holder.transform.localPosition = Target_Cam_Pos;
   }

}
