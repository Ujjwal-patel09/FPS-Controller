using System;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
  [SerializeField]FPS_Player_Movement fPS_Player_Movement;
  [SerializeField]Animator playerAnimator;

  // Player input from movement script
  private float Forward_Backward_Input;
  private float Right_Left_Input;
  
  private void Start() 
  {
    playerAnimator = GetComponent<Animator>();
    fPS_Player_Movement = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS_Player_Movement>(); 
  }
    
  void Update()
  {
    transform.localPosition = new Vector3(0,0,0);
     
    Player_Input();
    Walking_Animation();
    Running_Animation();
    Jump_Animation();
    Crouch_Walking_Animation();
  
  }
  
  private void Player_Input()
  {
    Forward_Backward_Input = fPS_Player_Movement.InputZ;
    Right_Left_Input = fPS_Player_Movement.InputX;
  }
  
  private void Idle_Animation(string yDirection_float_Name,string xDirection_float_Name)
  {
    float Idle = 0f;
    float transation_Time = 0.1f;
    playerAnimator.SetFloat(yDirection_float_Name,Idle,transation_Time,Time.deltaTime);
    playerAnimator.SetFloat(xDirection_float_Name,Idle,transation_Time,Time.deltaTime);
  }
  
  private void Play_Animation (string yDirection_float_Name , string xDirection_float_Name)
  {
    float transation_Time = 0.1f;
    float posative_Float = 1f;
    float Nagetive_Float =-1f;

    if(Forward_Backward_Input > 0.1f)
    {
      playerAnimator.SetFloat(yDirection_float_Name,posative_Float,transation_Time,Time.deltaTime);// front
    }
    else if(Forward_Backward_Input < -0.1f)
    {
      playerAnimator.SetFloat(yDirection_float_Name,Nagetive_Float ,transation_Time,Time.deltaTime);// back
    }
    else
    {
      playerAnimator.SetFloat(yDirection_float_Name,0,transation_Time,Time.deltaTime);// no_Animation
    }

    if(Right_Left_Input > 0.1f)
    {
      playerAnimator.SetFloat( xDirection_float_Name,posative_Float,transation_Time,Time.deltaTime);// left
    }
    else if(Right_Left_Input < -0.1f)
    {
      playerAnimator.SetFloat( xDirection_float_Name, Nagetive_Float ,transation_Time,Time.deltaTime);// Right
    }
    else
    {
      playerAnimator.SetFloat( xDirection_float_Name,0,transation_Time,Time.deltaTime);// no_Animation
    }  
  }
 
  private void Walking_Animation()
  {
    if(fPS_Player_Movement.iswalking == true)
    {
      Play_Animation("yWalk_Direction","xWalk_Direction");
    }else
    {
      Idle_Animation("yWalk_Direction","xWalk_Direction");
    }
  }

  private void Running_Animation()
  {
    if(fPS_Player_Movement.isrunning == true)
    {
      playerAnimator.SetBool("isRunning",true);
      Play_Animation("yRun_Direction","xRun_Direction");
    }else
    {
      playerAnimator.SetBool("isRunning",false);
    }
  }
  
  private void Jump_Animation()
  {
    if(fPS_Player_Movement.isJumping == true)
    {
      playerAnimator.SetBool("Jumping",true);
    }else
    {
      playerAnimator.SetBool("Jumping",false);
    }
  }

  private void Crouch_Walking_Animation()
  {
    if(fPS_Player_Movement.isCrouching)
    {
      playerAnimator.SetBool("isCrouching",true);
      Play_Animation("yCrouch_Direction","xCrouch_Direction");
    }
    else
    {
      playerAnimator.SetBool("isCrouching",false);
    }
  }

}
