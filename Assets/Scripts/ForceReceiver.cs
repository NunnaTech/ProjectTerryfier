using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField]
    private CharacterController Controller;
    private float verticalVelocity;
    public Vector3 Movement => Vector3.up * verticalVelocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(verticalVelocity < 0 && Controller.isGrounded)
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
       else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;

    }
}
