using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collection : MonoBehaviour
{
    public void Death(){
        FindObjectOfType<PlayerController>().CherryCount();
         Destroy(gameObject);
       // FindObjectOfType<PlayerController>().GemCount();
       // Destroy(gameObject);
    }
}
