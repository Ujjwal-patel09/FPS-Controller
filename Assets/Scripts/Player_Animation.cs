using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    public Animator Playeranimator;
    public bool iswalking;

    void Update()
    {
        if(iswalking)
        {
            Playeranimator.SetBool("isWalking",true);
        }else{
            Playeranimator.SetBool("isWalking",false);
        }
    }
}
