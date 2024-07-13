using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    public FPS_Player_Movement fPS_Player_Movement;
    public Animator playerAnimator;
    

    void Update()
    {
        transform.localPosition = new Vector3(0,0,0);

        // for walking animation //
        if(fPS_Player_Movement.MoveDirection.magnitude > 0.1f)
        {
            playerAnimator.SetBool("isWalking",true);
        }else
        {
           playerAnimator.SetBool("isWalking",false);
        }

        // for running animation //
        if(fPS_Player_Movement.MoveDirection.magnitude > 0.1f && Input.GetKey(KeyCode.LeftShift))
        {
            playerAnimator.SetBool("isRunning",true);
            fPS_Player_Movement.MoveSpeed = 6f;
        }else
        {
            playerAnimator.SetBool("isRunning",false);
            fPS_Player_Movement.MoveSpeed = 2f;
        }

        
    }
}
