using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpeedBoostPickup : MonoBehaviour
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

         Debug.Log("SpeedBoosted!") ;

         PlayerBehavior.Boost = 4f;

        gameManager.SpeedBoostEnabled = true;

         gameManager.Items += 1;
        }
    }
}
