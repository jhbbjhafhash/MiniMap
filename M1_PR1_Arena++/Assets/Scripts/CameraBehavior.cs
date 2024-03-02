using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
   //1
   public Vector3 camOffset = new Vector3(0f, 3.2f, -4.6f) ;

   //2
   private Transform target;

   void Start ()
   {
    //3
    target = GameObject.Find("Player").transform;
   }
 
    //4
    void LateUpdate ()
    {
        //5
        transform.position = target.TransformPoint(camOffset) ;

        //6
        transform.LookAt(target) ;
    }

}