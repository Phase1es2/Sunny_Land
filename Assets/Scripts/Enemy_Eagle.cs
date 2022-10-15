using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{

    private Rigidbody2D rb;
    //private Collider2D coll;
    public Transform top, bot;
    public float Speed;
    private float topY, botY;
    //private Animator Anim;
    private bool isUp = true;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
      //  coll = GetComponent<Collider2D>();
        //Anim = GetComponent<Animator>();
        topY = top.position.y;
        botY = bot.position.y;
        Destroy(top.gameObject);
        Destroy(bot.gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement(){
       if(isUp){
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if(transform.position.y > topY){
                isUp = false;
            }
        }else{
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if(transform.position.y < botY){
                isUp = true;
            }
        }
    }
/*
    void Death(){
        Destroy(gameObject);
    }

    public void JumpOnTop(){
        Anim.SetTrigger("death");
    }*/
}
