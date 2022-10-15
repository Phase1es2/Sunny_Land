using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D rb;
    public Collider2D coll;
    public Collider2D DisColl;
    public Transform CellingCheck, GroundCheck;
  //  public AudioSource jumpAudio, hurtAudio, cherryAudio;
    [Space]
    public float speed;
    public float jumpforce;
    [Space]
    public LayerMask ground;
    [SerializeField]
    public int Cherry;
    public Text CherryNum;
  //  public int Gem;
  //  public Text GemNum;
    private bool isHurt;
    private bool isGround;
    private int extraJump;
   // public Transform CellingCheck;
    //public AudioSource jumpAudio, hurtAudio, cherryAudio;

    //public Collider2D DisColl;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
    }

    // Update is called once per frame
    
    //void Update()
    void FixedUpdate(){
        if(!isHurt){
            Movement();
        }
        SwitchAnim();
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.2f, ground);
    }

    private void Update(){
       // Jump();
        Crouch();
        CherryNum.text = Cherry.ToString(); 
       // GemNum.text = Gem.ToString();
        newJump();
    }

    void Movement(){

        float horizontalmove = Input.GetAxis("Horizontal");
        //horizontalmove = Input.GetAxis("Horizontal");

        float facedirection = Input.GetAxisRaw("Horizontal");


        //player movment;
        if(horizontalmove != 0){
            //use rigidbody to set the velocity for the x dirction
            //using Time.deltaTime to fit the speed to differetn machine
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            //The input will be -1, 0, 1;
            // Running in Unity( less < 0.1   ); 
            anim.SetFloat("running", Mathf.Abs(facedirection));
        }

        //the funciton that change the facing direction for the player
        if(facedirection != 0){
            transform.localScale = new Vector3(facedirection, 1, 1);
        }

    /*
        //player jump 
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)){
            //use rigidboy to set the velocity for the y dirction 

            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);

        }

        Crouch();
        */
    }
/*
    void Jump(){
        if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground)){
            //use rigidboy to set the velocity for the y dirction 
ß
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.fixedDeltaTime);
            jumpAudio.Play();
            anim.SetBool("jumping", true);

        }
    }*/

    void newJump(){
        if(isGround){
            extraJump = 1;
        }
        if(Input.GetButtonDown("Jump") && extraJump > 0){
          
           rb.velocity = Vector2.up * jumpforce; //new Vector(0, 1)
           //rb.velocity = new Vector(0,1 * jum);
           // SoundManager soundManager = gameObject.GetComponent<SceneManagement>();
            //soundManager.JumpAudio();
            //rb.velocity = Vector2.up * JumpForce;
            SoundManager.instance.JumpAudio();
            extraJump--;
            anim.SetBool("jumping", true);
        }
        //if()
        
        if(Input.GetButtonDown("Jump") && extraJump == 0 && isGround){
            rb.velocity = Vector2.up * jumpforce;
             SoundManager.instance.JumpAudio();
            anim.SetBool("jumping", true);
        }
    }



    //Switch animation between jumping and falling, and idle
    //use rigidbody to check the collider
    void SwitchAnim(){
        anim.SetBool("idle", false);

        if(rb.velocity.y < 0.1f && !coll.IsTouchingLayers(ground)){
            anim.SetBool("falling", true);
        }

        if(anim.GetBool("jumping")){
            if(rb.velocity.y < 0){
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }else if(isHurt){
             anim.SetBool("hurt", true);
             anim.SetFloat("running", 0);
            if(Mathf.Abs(rb.velocity.x) < 0.1f){
                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                isHurt = false;
            }
        }else if(coll.IsTouchingLayers(ground)){
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }

    //Collection
    private void OnTriggerEnter2D(Collider2D other) {
         //Collection
        if(other.tag == "Collection"){
           // cherryAudio.Play();
           SoundManager.instance.CherryAudio();
            other.GetComponent<Animator>().Play("is_got");
            //Destroy(other.gameObject);
            //cherryAudio.Play();
           // SoundManager.instance.CherryAudio();
           // Cherry++;
            //CherryNum.text = Cherry.ToString();       
        }


        if(other.tag == "DeadLine"){
          //  GetComponent<AudioSource>().enabled = false;
            Invoke("Restart", 2f);
        }
    }

    //KILL ENEMIES
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Enemy"){
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
          //  Enemy_Eagle eagle = other.gameObject.GetComponent<Enemy_Eagle>();
            if(anim.GetBool("falling")){
              //Destroy(other.gameObject);
                enemy.JumpOn();
          //      eagle.JumpOnTop();
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }else if(transform.position.x < other.gameObject.transform.position.x){
                rb.velocity = new Vector2(-3, rb.velocity.y);
              //  hurtAudio.Play();
                 SoundManager.instance.HurtAudio();
                isHurt = true;
                
            }else if(transform.position.x > other.gameObject.transform.position.x){
                  rb.velocity = new Vector2(3, rb.velocity.y);
               //   hurtAudio.Play();
               SoundManager.instance.HurtAudio();
                  isHurt = true;
            }
        }
    }

    void Crouch(){
        if(!Physics2D.OverlapCircle(CellingCheck.position, 0.2f, ground)){
            if(Input.GetButton("Crouch")){
                anim.SetBool("crouching", true);
                DisColl.enabled = false;
            }else{
                 anim.SetBool("crouching", false);
                DisColl.enabled = true;
            }
        }
    }

    void Restart(){

        //if(other.tag == "DeadLine"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       // }
    }

    public void CherryCount(){
        Cherry++;
    }
/*
    public void GemCount(){
        Gem++;
    }
    */
}
