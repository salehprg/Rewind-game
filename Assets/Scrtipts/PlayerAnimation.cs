using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public PlayerData playerdata;
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Falling" , playerdata.falling);
        animator.SetBool("Rising" , playerdata.rising);
        animator.SetFloat("Speed" , playerdata.speed);
    }
}
