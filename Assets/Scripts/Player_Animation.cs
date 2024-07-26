using System;
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

    // for walking animation //
    if(fPS_Player_Movement.Walking)
    {
        playerAnimator.SetBool("isWalking",true);
    }else
    {
        playerAnimator.SetBool("isWalking",false);
    }

    // for running animation //
    if(fPS_Player_Movement.Running)
    {
        playerAnimator.SetBool("isRunning",true);    
    }else
    {
        playerAnimator.SetBool("isRunning",false);
    }
        
    // for jumping animation //
    if(fPS_Player_Movement.Jump)
    {
        playerAnimator.SetBool("isJump",true);
    }else
    {
        playerAnimator.SetBool("isJump",false);
    }
        
  }
}
