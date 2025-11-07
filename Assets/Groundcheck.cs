using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundcheck : MonoBehaviour
{
    PlayerLogic PlayerMovement;

    private void Start(){
        PlayerMovement = this.GetComponentInParent<PlayerLogic>();
    }

    private void OnTriggerEnter(Collider other){
        // Debug.Log("Touch The Ground");
        PlayerMovement.groundedchanger();
    }
}
