using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator Anim;
    protected AudioSource deathAudio;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    public void Death(){
        //deathAudio.Play();
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject);
     //   Anim.SetTrigger("death");
    }

    public void JumpOn(){
       // Destroy(gameObject);
        //deathAudio.Play();
        Anim.SetTrigger("death");
        deathAudio.Play();
    }
}
