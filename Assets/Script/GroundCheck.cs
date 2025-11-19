using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    PlayerLogic logicmovement;
    private void Start()
    {
        logicmovement = this.GetComponentInParent<PlayerLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Touch the ground");
        logicmovement.groundchanger();
    }
}
