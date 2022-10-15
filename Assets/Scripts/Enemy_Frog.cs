using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{

    private Rigidbody2D rb;
   
    public Transform leftpoint, rightpoint;
    public float Speed, JumpForce;
    private bool Faceleft = true;
    private float leftx, rightx;
    public LayerMask Ground;
    

   // private Animator Anim;
    private Collider2D Coll;

   // public float Speed;
    //Transform

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
     //   Anim = GetComponent<Animator>();
        Coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }


    // Update is called once per frame
    void Update(){
       // Movement();
       SwitchAnim();
    }

    void Movement(){
        if(Faceleft){
            if(Coll.IsTouchingLayers(Ground)){
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-Speed, JumpForce);
            }
            if(transform.position.x < leftx){
                transform.localScale = new Vector3(-1, 1, 1);
                Faceleft = false;
            }
        }else{
            if(Coll.IsTouchingLayers(Ground)){
                Anim.SetBool("jumping", true);
                rb.velocity = new Vector2(Speed, JumpForce);
            }
           // rb.velocity = new Vector2(Speed, JumpForce);
            if(transform.position.x > rightx){
                transform.localScale = new Vector3(1, 1, 1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim(){
        if(Anim.GetBool("jumping")){
            if(rb.velocity.y < 0.1f){
                Anim.SetBool("jumping", false);
                Anim.SetBool("falling", true);
            }
        }
        if(Coll.IsTouchingLayers(Ground) && Anim.GetBool("falling")){
            Anim.SetBool("falling", false);
        }
    }
/*
    void Death(){
        Destroy(gameObject);
     //   Anim.SetTrigger("death");
    }

    public void JumpOn(){
       // Destroy(gameObject);
        Anim.SetTrigger("death");
    }*/
}
