using UnityEngine;

public class Player_Animation : MonoBehaviour
{
  [SerializeField]FPS_Player_Movement fPS_Player_Movement;
  [SerializeField]Animator playerAnimator;

  private void Start() 
  {
    playerAnimator = GetComponent<Animator>();
    fPS_Player_Movement = GameObject.FindGameObjectWithTag("Player").GetComponent<FPS_Player_Movement>(); 
  }
    
  void Update()
  {
    transform.localPosition = new Vector3(0,0,0);

    // setting All the float value in blend tree 1 = Walking , 2 = Running //
    walking_Running_Animation();
    Jump_Animation();
    Crouch_Walking_Animation();
  
  }

  private void walking_Running_Animation()
  {
    if(fPS_Player_Movement.iswalking == true)
    {
      if(Input.GetKey(KeyCode.W))// for walking & running Front //
      {
        if(fPS_Player_Movement.isrunning == true)
        {
          playerAnimator.SetFloat("yWalk_Direction",2f,0.1f,Time.deltaTime);
        }else
        {
          playerAnimator.SetFloat("yWalk_Direction",1f,0.1f,Time.deltaTime);
        }
      }

      if(Input.GetKey(KeyCode.S))// for walking & running Backward //
      {
        if(fPS_Player_Movement.isrunning == true)
        {
          playerAnimator.SetFloat("yWalk_Direction",-2f,0.1f,Time.deltaTime);
        }else
        {
          playerAnimator.SetFloat("yWalk_Direction",-1f,0.1f,Time.deltaTime);
        }
      }

      if(Input.GetKey(KeyCode.A))// for walking & running Left //
      {
        if(fPS_Player_Movement.isrunning == true)
        {
          playerAnimator.SetFloat("xWalk_Direction",2f,0.1f,Time.deltaTime);
        }else
        {
          playerAnimator.SetFloat("xWalk_Direction",1f,0.1f,Time.deltaTime);
        }
      }

      if(Input.GetKey(KeyCode.D))// for walking & running Right //
      {
        if(fPS_Player_Movement.isrunning == true)
        {
          playerAnimator.SetFloat("xWalk_Direction",-2f,0.1f,Time.deltaTime);
        }else
        {
          playerAnimator.SetFloat("xWalk_Direction",-1f,0.1f,Time.deltaTime);
        }
      }
    }else
    {
      // stay Idle //
      playerAnimator.SetFloat("yWalk_Direction",0,0.1f,Time.deltaTime);
      playerAnimator.SetFloat("xWalk_Direction",0,0.1f,Time.deltaTime);
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
      if(Input.GetKey(KeyCode.W))
      {
        playerAnimator.SetFloat("yCrouch_Direction",1,0.1f,Time.deltaTime);
      }
      else if(Input.GetKey(KeyCode.S))
      {
        playerAnimator.SetFloat("yCrouch_Direction",-1,0.1f,Time.deltaTime);
      }
      else if(Input.GetKey(KeyCode.A))
      {
        playerAnimator.SetFloat("xCrouch_Direction",1,0.1f,Time.deltaTime);
      }
      else if(Input.GetKey(KeyCode.D))
      {
        playerAnimator.SetFloat("xCrouch_Direction",-1,0.1f,Time.deltaTime);
      }
      else
      {
        playerAnimator.SetFloat("xCrouch_Direction",0,0.1f,Time.deltaTime);
        playerAnimator.SetFloat("yCrouch_Direction",0,0.1f,Time.deltaTime);
      }
    }
    else
    {
      playerAnimator.SetBool("isCrouching",false);
    }
  }

}
