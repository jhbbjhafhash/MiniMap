using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JumpBoostPickup : MonoBehaviour
{
    public GameBehavior gameManager;

    public PlayerBehavior PlayerBehavior;

    void Start ()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();

        PlayerBehavior = GameObject.Find("Player").GetComponent<PlayerBehavior>();     
    }

    void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
         Destroy(transform.parent.gameObject) ;

         Debug.Log("JumpBoosted!") ;

         PlayerBehavior.JumpBoost = 3f;

         gameManager.JumpBoostEnabled = true;

         gameManager.Items += 1;
        }
    }
}
